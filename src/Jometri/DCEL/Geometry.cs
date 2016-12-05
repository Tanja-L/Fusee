﻿using System;
using System.Collections.Generic;
using System.Linq;
using Fusee.Base.Core;
using Fusee.Jometri.Triangulation;
using Fusee.Math.Core;

namespace Fusee.Jometri.DCEL
{
    /// <summary>
    /// Each vertex contains:
    /// A handle to assign a abstract reference to it.
    /// The vertex' coordinates.
    /// </summary>
    public struct Vertex
    {
        /// <summary>
        /// The vertex' reference.
        /// </summary>
        public VertHandle Handle;

        /// <summary>
        /// The geometric data of the vertex
        /// </summary>
        public float3 Coord;

        /// <summary>
        /// The handle to the half edge with this vertex as origin
        /// </summary>
        public HalfEdgeHandle IncidentHalfEdge;


        /// <summary>
        /// The vertex' constuctor.
        /// </summary>
        /// <param name="coord">The new vertex' coordinates</param>
        public Vertex(float3 coord)
        {
            Handle = new VertHandle();
            IncidentHalfEdge = new HalfEdgeHandle();
            Coord = coord;
        }

        public void ChangeCoord(float3 newCoord)
        {
            Coord = newCoord;
        }
    }

    /// <summary>
    /// Represents a half edge.
    /// Each half edge contains:
    /// A handle to assign a abstract reference to it.
    /// A handle to the half edges' origin vertex.
    /// A handle to the next half edge (in ccw order).
    /// A handle to the previous half edge (in ccw order).
    /// A handle to the face it belongs to.
    /// </summary>
    internal struct HalfEdge
    {
        internal HalfEdgeHandle Handle;

        internal VertHandle Origin;
        internal HalfEdgeHandle Twin;
        internal HalfEdgeHandle Next;
        internal HalfEdgeHandle Prev;
        internal FaceHandle IncidentFace;
    }

    /// <summary>
    /// Each face contains:
    /// A handle to assign a abstract reference to it.
    /// A handle to one of the half edges that belongs to this faces outer boundary.
    /// A List that contains handles to one half edge for each hole in a face
    /// Note that unbounded faces can't have a OuterHalfEdge but need to have at least one InnerHalfEdge - bounded faces must have a OuterComponent
    /// </summary>
    internal struct Face //TODO: Create 2D and 3D Face - difference: a 3D face can not have inner half edges!
    {
        internal FaceHandle Handle;
        internal HalfEdgeHandle OuterHalfEdge;
        internal List<HalfEdgeHandle> InnerHalfEdges;
    }

    /// <summary>
    /// Represents a outer or inner boundary of a face
    /// </summary>
    public struct Outline
    {
        /// <summary>
        /// The geometric information of the vertices which belong to a boundary
        /// </summary>
        public IList<float3> Points;

        /// <summary>
        /// Determines wheather a boundary is a outer bondary or a inner boundary (which forms a hole in the face).
        /// </summary>
        public bool IsOuter;
    }

    /// <summary>
    /// Geometry, stored in a DCEL (doubly conneted edge list).
    /// </summary>
    public class Geometry
    {
        #region Members

        /// <summary>
        /// Contains handles to half edges. Use this to find adjecant half edges or the origin vertex.
        /// </summary>
        public IList<HalfEdgeHandle> HalfEdgeHandles;

        /// <summary>
        /// Contains handles to outlines. Use this to find the first half edge.
        /// </summary>
        public IList<FaceHandle> FaceHandles;

        /// <summary>
        /// Contains handles to outline. Use this to get the vertexes coordinates.
        /// </summary>
        public IList<VertHandle> VertHandles;

        internal List<Vertex> Vertices { get; }

        internal List<HalfEdge> HalfEdges { get; }

        internal List<Face> Faces { get; }

        private int _highestVertHandle;
        private int _highestHalfEdgeHandle;
        private int _highestFaceHandle;

        #endregion

        /// <summary>
        /// 2D Geometry, stored in a DCEL (doubly conneted edge list).
        /// </summary>
        /// <param name="outlines">A collection of the geometrys' outlines, each containing the geometric information as a list of float3 in ccw order</param>
        /// <param name="triangulate">If triangulate is set to true, the created geometry will be triangulated</param>
        public Geometry(IEnumerable<Outline> outlines, bool triangulate = false)
        {
            Vertices = new List<Vertex>();
            HalfEdges = new List<HalfEdge>();
            Faces = new List<Face>();

            HalfEdgeHandles = new List<HalfEdgeHandle>();
            FaceHandles = new List<FaceHandle>();
            VertHandles = new List<VertHandle>();

            CreateHalfEdgesFor2DGeometry(outlines);

            if (triangulate)
                this.Triangulate();
        }

        /// <summary>
        /// Creates an empty geometry, that can be filled by the user using InsertFace, InsertHalfEdge and InsertVertex methodes
        /// </summary>
        public Geometry()
        {
            Vertices = new List<Vertex>();
            HalfEdges = new List<HalfEdge>();
            Faces = new List<Face>();

            HalfEdgeHandles = new List<HalfEdgeHandle>();
            FaceHandles = new List<FaceHandle>();
            VertHandles = new List<VertHandle>();
        }

        //Clones a existing Geometry object. E.g to create a backface for an extrusion
        internal Geometry CloneGeometry()
        {
            var clone = new Geometry();
            
            foreach (var vHandle in VertHandles)
            {
                clone.VertHandles.Add(vHandle);
            }

            
            foreach (var vert in Vertices)
            {
                clone.Vertices.Add(vert);
            }

            
            foreach (var heHandle in HalfEdgeHandles)
            {
                clone.HalfEdgeHandles.Add(heHandle);
            }
            
            foreach (var he in HalfEdges)
            {
                clone.HalfEdges.Add(he);
            }

            
            foreach (var fHandle in FaceHandles)
            {
                clone.FaceHandles.Add(fHandle);
            }
            
            foreach (var f in Faces)
            {
                clone.Faces.Add(f);
            }
            return clone;
        }

        #region public Methods

        /// <summary>
        /// Inserts a new face between vertices of the geometry
        /// </summary>
        /// <param name="vHandles">The vertices of the new face, they have to be in ccw order (according to the orientation of the new face)</param>
        public void InsertFace(IList<VertHandle> vHandles)
        {
            //TODO: create twins and check if halfedges are already part of the geometry
            var faceHandle = new FaceHandle(FaceHandles.Count + 1);
            var face = new Face
            {
                Handle = faceHandle,
                InnerHalfEdges = new List<HalfEdgeHandle>()
            };

            var faceHalfEdges = new List<HalfEdge>();

            foreach (var vertHandle in vHandles)
            {
                var halfEdgeHandle = new HalfEdgeHandle(HalfEdgeHandles.Count + 1);
                HalfEdgeHandles.Add(halfEdgeHandle);
                var halfEdge = new HalfEdge
                {
                    Origin = vertHandle,
                    Handle = halfEdgeHandle,
                    IncidentFace = face.Handle
                };

                faceHalfEdges.Add(halfEdge);
            }

            var origHalfEdges = new List<HalfEdge>();
            origHalfEdges.AddRange(HalfEdges);

            for (var i = 0; i < faceHalfEdges.Count; i++)
            {
                //Assign face.OuterHalfEdge
                if (i == 0)
                {
                    face.OuterHalfEdge = faceHalfEdges[i].Handle;
                    Faces.Add(face);
                    FaceHandles.Add(face.Handle);
                }

                var current = faceHalfEdges[i];
                current.Next = faceHalfEdges[(i + 1) % faceHalfEdges.Count].Handle;
                current.Prev = i - 1 >= 0 ? faceHalfEdges[i - 1].Handle : faceHalfEdges.LastItem().Handle;

                //Check if new HalfEdge is a twin to some other (already existing) HalfEdge in this face //TODO optimize by ceckeing only those half edges targeting to origin vert of new HalfEdge
                var curOrigin = current.Origin;
                var curTarget = new VertHandle();
                foreach (var hEdge in faceHalfEdges)
                {
                    if (current.Next != hEdge.Handle) continue;
                    curTarget = hEdge.Origin;
                    break;
                }

                for (var j = 0; j < origHalfEdges.Count; j++)
                {
                    var he = origHalfEdges[j];
                    var heOrigin = he.Origin;
                    var heTarget = GetHalfEdgeByHandle(he.Next).Origin;

                    if (heOrigin == curTarget && heTarget == curOrigin && he.Twin == default(HalfEdgeHandle))
                    {
                        current.Twin = he.Handle;
                        he.Twin = current.Handle;

                        HalfEdges[j] = he;
                        break;
                    }
                    if (heOrigin == curOrigin && heTarget == curTarget)
                    {
                        throw new DublicatedHalfEdgeException("HalfEdge with origin vertex " + heOrigin.Id +
                                                              " and target vertex " +
                                                              heTarget.Id + " already exists");
                    }
                }
                HalfEdges.Add(current);
            }
        }

        /// <summary>
        /// Inserts a pair of half edges between two (non adjacant) vertices of a face.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <exception cref="Exception"></exception>
        public void InsertDiagonal(VertHandle p, VertHandle q)
        {
            var vertP = GetVertexByHandle(p);
            var vertQ = GetVertexByHandle(q);

            var heStartingAtP = GetHalfEdgesStartingAtV(vertP).ToList();
            var heStaringAtQ = GetHalfEdgesStartingAtV(vertQ).ToList();

            var face = new Face();
            var pStartHe = new HalfEdge();
            var qStartHe = new HalfEdge();

            foreach (var heP in heStartingAtP)
            {
                var faceHandleP = GetHalfEdgeByHandle(heP).IncidentFace;

                foreach (var heQ in heStaringAtQ)
                {
                    var faceHandleQ = GetHalfEdgeByHandle(heQ).IncidentFace;

                    if (faceHandleP != faceHandleQ) continue;

                    var commonFace = GetFaceByHandle(faceHandleP);

                    if (commonFace.OuterHalfEdge == default(HalfEdgeHandle)) break;

                    face = GetFaceByHandle(faceHandleP);
                    pStartHe = GetHalfEdgeByHandle(heP);
                    qStartHe = GetHalfEdgeByHandle(heQ);

                    break;
                }
            }
            if (pStartHe.Handle == default(HalfEdgeHandle))
                throw new ArgumentException("Vertex " + p + " vertex " + q + " have no common Face!");

            if (IsVertexAdjacentToVertex(p, q, pStartHe, qStartHe))
                throw new ArgumentException("A diagonal can not be insertet beween adjacent Vertices!");

            var holes = GetHoles(face);

            var newFromP = new HalfEdge();
            var newFromQ = new HalfEdge();

            newFromP.Origin = p;
            newFromP.Next = qStartHe.Handle;
            newFromP.Prev = pStartHe.Prev;
            newFromP.IncidentFace = face.Handle;
            newFromP.Handle = new HalfEdgeHandle(CreateHalfEdgeHandleId());
            HalfEdgeHandles.Add(newFromP.Handle);

            newFromQ.Origin = q;
            newFromQ.Next = pStartHe.Handle;
            newFromQ.Prev = qStartHe.Prev;
            newFromQ.IncidentFace = face.Handle;
            newFromQ.Handle = new HalfEdgeHandle(CreateHalfEdgeHandleId());
            HalfEdgeHandles.Add(newFromQ.Handle);

            newFromP.Twin = newFromQ.Handle;
            newFromQ.Twin = newFromP.Handle;

            HalfEdges.Add(newFromP);
            HalfEdges.Add(newFromQ);

            //Assign new Next to previous HalfEdges from p and q & assign new prev for qStartHe and pStartHe
            var prevHeP = GetHalfEdgeByHandle(pStartHe.Prev);
            var prevHeQ = GetHalfEdgeByHandle(qStartHe.Prev);
            var count = 0;
            for (var i = 0; i < HalfEdges.Count; i++)
            {
                var he = HalfEdges[i];
                if (he.Handle == prevHeP.Handle)
                {
                    he.Next = newFromP.Handle;
                    HalfEdges[i] = he;
                    count++;
                }
                else if (he.Handle == prevHeQ.Handle)
                {
                    he.Next = newFromQ.Handle;
                    HalfEdges[i] = he;
                    count++;
                }
                else if (HalfEdges[i].Handle == pStartHe.Handle)
                {
                    he.Prev = newFromQ.Handle;
                    HalfEdges[i] = he;
                    count++;
                }
                else if (HalfEdges[i].Handle == qStartHe.Handle)
                {
                    he.Prev = newFromP.Handle;
                    HalfEdges[i] = he;
                    count++;
                }
                if (count == 4) break;
            }

            if (holes.Count != 0 && IsHalfEdgeToHole(holes, p, q, face)) return;

            var newFace = new Face
            {
                Handle = new FaceHandle(CreateFaceHandleId()),
                InnerHalfEdges = new List<HalfEdgeHandle>()
            };
            FaceHandles.Add(newFace.Handle);

            newFace.OuterHalfEdge = newFromQ.Handle;
            Faces.Add(newFace);

            //Assign the handle of the new face to its half edges
            AssignFaceHandle(newFace.OuterHalfEdge, newFace);

            //Set face.OuterHalfEdge to newFromP - old OuterHalfEdge can be part of new face now!
            for (var i = 0; i < Faces.Count; i++)
            {
                if (Faces[i].Handle != face.Handle) continue;

                var firstHe = Faces[i];
                firstHe.OuterHalfEdge = newFromP.Handle;
                Faces[i] = firstHe;
            }
        }

        /// <summary>
        /// Gets a vertex by its handle
        /// </summary>
        /// <param name="vHandle">The vertex' reference</param>
        /// <returns></returns>
        public Vertex GetVertexByHandle(VertHandle vHandle)
        {
            foreach (var e in Vertices)
            {
                if (e.Handle == vHandle)
                    return e;
            }
            throw new HandleNotFoundException("HalfEdge with id " + vHandle.Id + " not found!");
        }

        internal HalfEdge GetHalfEdgeByHandle(HalfEdgeHandle hehandle)
        {
            foreach (var e in HalfEdges)
            {
                if (e.Handle.Id == hehandle.Id)
                    return e;
            }
            throw new HandleNotFoundException("HalfEdge with id " + hehandle.Id + " not found!");
        }

        internal Face GetFaceByHandle(FaceHandle fHandle)
        {
            foreach (var e in Faces)
            {
                if (e.Handle.Id == fHandle.Id)
                    return e;
            }
            throw new HandleNotFoundException("HalfEdge with id " + fHandle.Id + " not found!");
        }

        #endregion

        #region circulators 
        //TODO: insert circulators:
        //iterate over all neighboring vertices to a vertex.
        //iterate over all incident edges to a vertex.
        //iterate over all adjacent faces to a vertex

        //iterate over the face's vertices
        //iterate over all edge-neighboring faces

        /// <summary>
        /// This collection contains all Vertices of a certain face.
        /// </summary>
        /// <param name="fHandle">The faces reference</param>
        /// <returns></returns>
        public IEnumerable<Vertex> GetFaceVertices(FaceHandle fHandle)
        {
            //Outer Outline
            var fistHalfEdgeHandle = GetFaceByHandle(fHandle).OuterHalfEdge;
            var halfEdgeOuter = GetHalfEdgeByHandle(fistHalfEdgeHandle);

            do
            {
                var originVert = halfEdgeOuter.Origin;
                yield return GetVertexByHandle(originVert);
                halfEdgeOuter = GetHalfEdgeByHandle(halfEdgeOuter.Next);

            } while (halfEdgeOuter.Handle != fistHalfEdgeHandle);

            //Inner Outlines
            var innerComponents = GetFaceByHandle(fHandle).InnerHalfEdges;

            if (innerComponents.Count == 0) yield break;

            foreach (var comp in innerComponents)
            {
                var halfEdgeInner = GetHalfEdgeByHandle(comp);

                do
                {
                    var originVert = halfEdgeInner.Origin;
                    yield return GetVertexByHandle(originVert);
                    halfEdgeInner = GetHalfEdgeByHandle(halfEdgeInner.Next);

                } while (halfEdgeInner.Handle != comp);

            }
        }

        /// <summary>
        /// This collection contains all handles to HalfEdges of a certain face.
        /// </summary>
        /// <param name="fHandle">The faces reference</param>
        /// <returns></returns>
        public IEnumerable<HalfEdgeHandle> GetHalfEdgesOfFace(FaceHandle fHandle)
        {
            var face = GetFaceByHandle(fHandle);
            var firstHandle = face.OuterHalfEdge;
            var current = GetHalfEdgeByHandle(face.OuterHalfEdge);
            do
            {
                yield return current.Handle;
                current = GetHalfEdgeByHandle(current.Next);
            } while (firstHandle != current.Handle);

            foreach (var he in face.InnerHalfEdges)
            {
                var cur = GetHalfEdgeByHandle(he);
                do
                {
                    yield return cur.Handle;
                    cur = GetHalfEdgeByHandle(cur.Next);

                } while (he != cur.Handle);
            }
        }

        /// <summary>
        /// This collection contains all handles to HalfEdges starting at a certain vertex.
        /// </summary>
        /// <param name="vert">The vertex reference</param>
        /// <returns></returns>
        public IEnumerable<HalfEdgeHandle> GetHalfEdgesStartingAtV(Vertex vert)
        {
            var e = GetHalfEdgeByHandle(vert.IncidentHalfEdge);
            var startEdge = e;

            yield return startEdge.Handle;

            while (TwinNext(e).Handle != startEdge.Handle)
            {
                e = TwinNext(e);
                yield return e.Handle;
            }
        }

        /// <summary>
        /// This collection contains all handles to HalfEdges ending at a certain vertex.
        /// </summary>
        /// <param name="vert">The vertex reference</param>
        /// <returns></returns>
        public IEnumerable<HalfEdgeHandle> GetHalfEdgesTargetingV(Vertex vert)
        {
            var e = GetHalfEdgeByHandle(vert.IncidentHalfEdge);
            var startEdge = e;

            yield return e.Twin;

            while (TwinNext(e).Handle != startEdge.Handle)
            {
                e = TwinNext(e);
                yield return e.Twin;
            }
        }

        //Used in GetHalfEdgesStartingAtV() and GetHalfEdgesTargetingV()
        private HalfEdge TwinNext(HalfEdge halfEdge)
        {
            if (halfEdge.Twin == default(HalfEdgeHandle))
                return default(HalfEdge);

            var twin = GetHalfEdgeByHandle(halfEdge.Twin);
            return GetHalfEdgeByHandle(twin.Next);
        }

        /// <summary>
        /// This collection contains all handles to HalfEdges belonging to a closed loop.
        /// </summary>
        /// <param name="heHandle">The reference to the HalfEdge with which the loop starts.</param>
        /// <returns></returns>
        public IEnumerable<HalfEdgeHandle> GetEdgeLoop(HalfEdgeHandle heHandle)
        {
            var currentHandle = heHandle;

            do
            {
                var currentHalfEdge = GetHalfEdgeByHandle(currentHandle);
                currentHandle = currentHalfEdge.Next;
                yield return currentHalfEdge.Handle;

            } while (currentHandle != heHandle);
        }
        #endregion
        
        #region internal methods for updating a certain HalfEdge, Vertex or Face
        internal void UpdateVertex(Vertex vert)
        {
            var handle = vert.Handle;
            for (var i = 0; i < Vertices.Count; i++)
            {
                var v = Vertices[i];
                if (v.Handle != handle) continue;
                Vertices[i] = vert;
                break;
            }
        }

        internal void UpdateFace(Face face)
        {
            var handle = face.Handle;
            for (var i = 0; i < Faces.Count; i++)
            {
                var f = Faces[i];
                if (f.Handle != handle) continue;
                Faces[i] = face;
                break;
            }
        }

        internal void UpdateHalfEdge(HalfEdge halfEdge)
        {
            var handle = halfEdge.Handle;
            for (var i = 0; i < HalfEdges.Count; i++)
            {
                var f = HalfEdges[i];
                if (f.Handle != handle) continue;
                HalfEdges[i] = halfEdge;
                break;
            }
        }

        #endregion

        //TODO: 3D geometry initialisation
        #region 2D geometry initialisation

        private void CreateHalfEdgesFor2DGeometry(IEnumerable<Outline> outlines)
        {

            var faceHandle = new FaceHandle { Id = FaceHandles.Count + 1 };
            var unboundedFace = new Face
            {
                Handle = faceHandle,
                OuterHalfEdge = new HalfEdgeHandle(),
                InnerHalfEdges = new List<HalfEdgeHandle>()
            };
            FaceHandles.Add(unboundedFace.Handle);
            Faces.Add(unboundedFace);

            foreach (var o in outlines)
            {
                var outlineHalfEdges = CreateHalfEdgesForBoundary(o);

                foreach (var he in outlineHalfEdges)
                {

                    //TODO: check for dublicates in outlineHalfEdges / _halfEdges - can only be done if HalfEdge.Next is known because this check needs the target vertex


                    HalfEdgeHandles.Add(he.Handle);
                    HalfEdges.Add(he);
                }
            }

            //Set highestHandles to Id of last the item in the handle lists
            SetHighestHandles();
        }

        private IEnumerable<HalfEdge> CreateHalfEdgesForBoundary(Outline outline)
        {
            var outlineHalfEdges = new List<HalfEdge>();
            var faceHandle = new FaceHandle();

            for (var i = 0; i < outline.Points.Count; i++)
            {
                var coord = outline.Points[i];
                bool isOldVert;
                var vert = CreateOrFindVertex(coord, out isOldVert);

                var halfEdgeHandle = new HalfEdgeHandle(CreateHalfEdgeHandleId());
                _highestHalfEdgeHandle = halfEdgeHandle.Id;

                if (!isOldVert)
                {
                    //Only necessary for new Vertices
                    vert.IncidentHalfEdge = halfEdgeHandle;
                    Vertices.Add(vert);
                }

                var halfEdge = new HalfEdge
                {
                    Origin = vert.Handle,
                    Handle = halfEdgeHandle,
                    Twin = new HalfEdgeHandle()
                };

                //Assumption: outlines are processed from outer to inner for every face, therfore faceHandle will never has its default value if "else" is hit.
                if (outline.IsOuter)
                {
                    if (faceHandle == default(FaceHandle))
                    {
                        Face face;
                        faceHandle = AddFace(halfEdge.Handle, out face);
                        FaceHandles.Add(faceHandle);
                        Faces.Add(face);
                    }
                }
                else
                {
                    if (i == 0)
                        Faces.LastItem().InnerHalfEdges.Add(halfEdge.Handle);

                    faceHandle = Faces.LastItem().Handle;
                }

                halfEdge.IncidentFace = faceHandle;
                outlineHalfEdges.Add(halfEdge);
            }

            for (var i = 0; i < outlineHalfEdges.Count; i++)
            {
                var he = outlineHalfEdges[i];

                //Assumption: a boundary is always closed!
                if (i + 1 < outlineHalfEdges.Count)
                    he.Next.Id = outlineHalfEdges[i + 1].Handle.Id;
                else { he.Next.Id = outlineHalfEdges[0].Handle.Id; }

                if (i - 1 < 0)
                    he.Prev.Id = outlineHalfEdges.LastItem().Handle.Id;
                else { he.Prev.Id = outlineHalfEdges[i - 1].Handle.Id; }

                outlineHalfEdges[i] = he;
            }

            outlineHalfEdges.AddRange(CreateOutlineTwins(outlineHalfEdges));

            return outlineHalfEdges;
        }

        private IEnumerable<HalfEdge> CreateOutlineTwins(IList<HalfEdge> outlineHalfEdges)
        {
            var twinHalfEdges = new List<HalfEdge>();

            for (var i = 0; i < outlineHalfEdges.Count; i++)
            {
                var oldhalfEdge = outlineHalfEdges[i];
                var halfEdgeHandle = new HalfEdgeHandle(CreateHalfEdgeHandleId());
                _highestHalfEdgeHandle = halfEdgeHandle.Id;

                //origin vertex for the twin is the origin vertex of the next half edge
                var originVert = outlineHalfEdges[(i + 1) % outlineHalfEdges.Count].Origin;

                var newHalfEdge = new HalfEdge
                {
                    Origin = originVert,
                    Handle = halfEdgeHandle,
                    Twin = oldhalfEdge.Handle,
                };
                var unboundFace = Faces.First();
                newHalfEdge.IncidentFace = unboundFace.Handle;

                twinHalfEdges.Add(newHalfEdge);

                //assign twin to "old" half edge
                oldhalfEdge.Twin = newHalfEdge.Handle;
                outlineHalfEdges[i] = oldhalfEdge;

                //Add fist half edge in loop to unboundFace InnerComponents
                if (i == 0)
                    unboundFace.InnerHalfEdges.Add(newHalfEdge.Handle);
            }

            //assign next and prev half edges
            for (var i = 0; i < twinHalfEdges.Count; i++)
            {
                var he = twinHalfEdges[i];

                //Assumption: a boundary is always closed!
                if (i + 1 < twinHalfEdges.Count)
                    he.Prev.Id = twinHalfEdges[i + 1].Handle.Id;
                else { he.Prev.Id = twinHalfEdges[0].Handle.Id; }

                if (i - 1 < 0)
                    he.Next.Id = twinHalfEdges.LastItem().Handle.Id;
                else { he.Next.Id = twinHalfEdges[i - 1].Handle.Id; }

                twinHalfEdges[i] = he;
            }

            return twinHalfEdges;
        }

        private FaceHandle AddFace(HalfEdgeHandle firstHalfEdge, out Face face)
        {
            var faceHandle = new FaceHandle { Id = FaceHandles.Count + 1 };

            face = new Face
            {
                Handle = faceHandle,
                OuterHalfEdge = firstHalfEdge,
                InnerHalfEdges = new List<HalfEdgeHandle>()
            };
            return faceHandle;
        }

        private Vertex CreateOrFindVertex(float3 pointCoord, out bool isOldVertex)
        {
            var vertHandle = new VertHandle();
            Vertex vert;

            //Check if a Vertex already exists and assign it to the HalfEdge instead of createing a new
            if (Vertices.Count != 0)
            {
                foreach (var v in Vertices)
                {
                    if (!pointCoord.Equals(v.Coord)) continue;
                    vertHandle.Id = v.Handle.Id;
                    isOldVertex = true;
                    return v;
                }

                //Create Vertice and VertHandle
                vertHandle.Id = VertHandles.Count + 1;
                VertHandles.Add(vertHandle);
                vert = new Vertex(pointCoord);
                vert.Handle = vertHandle;
            }
            else
            {
                //Create Vertices and VertHandle
                vertHandle.Id = VertHandles.Count + 1;
                VertHandles.Add(vertHandle);
                vert = new Vertex(pointCoord);
                vert.Handle = vertHandle;
            }
            isOldVertex = false;
            return vert;
        }
        
        #endregion

        #region methods concerning InsertDiagonal

        private Dictionary<HalfEdgeHandle, List<HalfEdgeHandle>> GetHoles(Face face)
        {
            var holes = new Dictionary<HalfEdgeHandle, List<HalfEdgeHandle>>();

            foreach (var he in face.InnerHalfEdges)
            {
                holes.Add(he, GetEdgeLoop(he).ToList());
            }

            return holes;
        }

        //Vertices need to be reduced to 2D
        //see Akenine-Möller, Tomas; Haines, Eric; Hoffman, Naty (2016): Real-Time Rendering, p. 754
        private bool IsPointInPolygon(FaceHandle fHandle, Vertex v)
        {
            v.Coord = v.Coord.Reduce2D();

            var inside = false;
            var faceVerts = GetFaceVertices(fHandle).ToList();

            var e0 = faceVerts.LastItem();
            e0.Coord = e0.Coord.Reduce2D();

            var y0 = e0.Coord.y >= v.Coord.y;

            foreach (var vert in faceVerts)
            {
                var e1 = vert;
                e1.Coord = e1.Coord.Reduce2D();

                var y1 = e1.Coord.y >= v.Coord.y;
                if (y0 != y1)
                {
                    if ((e1.Coord.y - v.Coord.y) * (e0.Coord.x - e1.Coord.x) >=
                        (e1.Coord.x - v.Coord.x) * (e0.Coord.y - e1.Coord.y) == y1)
                    {
                        inside = !inside;
                    }
                }
                y0 = y1;
                e0 = e1;
            }
            return inside;
        }

        private void AssignFaceHandle(HalfEdgeHandle heHandle, Face newFace)
        {
            var oldFaceHandle = GetHalfEdgeByHandle(heHandle).IncidentFace;
            var currentHe = GetHalfEdgeByHandle(heHandle);
            do
            {
                currentHe.IncidentFace = newFace.Handle;

                for (var i = 0; i < HalfEdges.Count; i++)
                {
                    if (HalfEdges[i].Handle != currentHe.Handle) continue;
                    HalfEdges[i] = currentHe;
                    break;
                }
                currentHe = GetHalfEdgeByHandle(currentHe.Next);
            } while (currentHe.Handle != heHandle);

            //Assign newFace to possible holes in the "old" face

            var oldFace = GetFaceByHandle(oldFaceHandle);
            if (oldFace.InnerHalfEdges.Count == 0) return;

            var inner = new List<HalfEdgeHandle>();
            inner.AddRange(oldFace.InnerHalfEdges);

            foreach (var heh in inner)
            {
                var origin = GetHalfEdgeByHandle(heh).Origin;

                if (!IsPointInPolygon(newFace.Handle, GetVertexByHandle(origin))) continue;

                oldFace.InnerHalfEdges.Remove(heh);
                newFace.InnerHalfEdges.Add(heh);

                var curHe = GetHalfEdgeByHandle(heh);
                do
                {
                    curHe.IncidentFace = newFace.Handle;

                    for (var i = 0; i < HalfEdges.Count; i++)
                    {
                        if (HalfEdges[i].Handle != curHe.Handle) continue;
                        HalfEdges[i] = curHe;
                        break;
                    }
                    curHe = GetHalfEdgeByHandle(curHe.Next);

                } while (curHe.Handle != heh);
            }
        }

        private bool IsHalfEdgeToHole(Dictionary<HalfEdgeHandle, List<HalfEdgeHandle>> holes, VertHandle p, VertHandle q, Face face)
        {
            if (holes.Count == 0) return false;

            foreach (var hole in holes)
            {
                foreach (var heHandle in hole.Value)
                {
                    var he = GetHalfEdgeByHandle(heHandle);
                    if (p != he.Origin && q != he.Origin) continue;

                    face.InnerHalfEdges.Remove(hole.Key);
                    return true;
                }
            }
            return false;
        }

        private bool IsVertexAdjacentToVertex(VertHandle p, VertHandle q, HalfEdge vertPStartHe, HalfEdge vertQStartHe)
        {
            var nextHeP = GetHalfEdgeByHandle(vertPStartHe.Next);
            var nextHeQ = GetHalfEdgeByHandle(vertQStartHe.Next);

            return nextHeP.Origin == q || nextHeQ.Origin == p;
        }
        #endregion
        
        #region methods concerning the creation of IDs for Vert-, Face- and HalfEdgeHandles

        internal int CreateVertHandleId()
        {
            var newId = _highestVertHandle + 1;
            _highestVertHandle = newId;
            return newId;
        }

        internal int CreateHalfEdgeHandleId()
        {
            var newId = _highestHalfEdgeHandle + 1;
            _highestHalfEdgeHandle = newId;
            return newId;
        }

        internal int CreateFaceHandleId()
        {
            var newId = _highestFaceHandle + 1;
            _highestFaceHandle = newId;
            return newId;
        }

        internal void SetHighestHandles()
        {
            _highestVertHandle = VertHandles.LastItem().Id;
            _highestHalfEdgeHandle = HalfEdgeHandles.LastItem().Id;
            _highestFaceHandle = FaceHandles.LastItem().Id;
        }

        #endregion

        
    }
}



