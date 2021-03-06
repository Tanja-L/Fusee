//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.8
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace C4d {

public class GvPort : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal GvPort(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(GvPort obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          throw new global::System.MethodAccessException("C++ destructor does not have public access");
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public void RemoveConnection() {
    C4dApiPINVOKE.GvPort_RemoveConnection(swigCPtr);
  }

  public int GetNrOfConnections() {
    int ret = C4dApiPINVOKE.GvPort_GetNrOfConnections(swigCPtr);
    return ret;
  }

  public void RemovePortConnections() {
    C4dApiPINVOKE.GvPort_RemovePortConnections(swigCPtr);
  }

  public bool IsIncomingConnected() {
    bool ret = C4dApiPINVOKE.GvPort_IsIncomingConnected(swigCPtr);
    return ret;
  }

  public bool GetIncomingDestination(ref GvNode /* GvNode_cstype */ node, ref GvPort /* GvPort_cstype */ port) {
    global::System.IntPtr p_node;
    unsafe { void *pp_node = &p_node;    /* GvNode_csin_pre */
    global::System.IntPtr p_port;
    unsafe { void *pp_port = &p_port;    /* GvPort_csin_pre */
    try {
      bool ret = C4dApiPINVOKE.GvPort_GetIncomingDestination(swigCPtr, (global::System.IntPtr) pp_node /* GvNode_csin */, (global::System.IntPtr) pp_port /* GvPort_csin */);
      if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } finally {
        node = new GvNode(p_node, false);
        /* GvNode_csin_post */
        port = new GvPort(p_port, false);
        /* GvPort_csin_post */
    }
} /* GvPort_csin_terminator */
} /* GvNode_csin_terminator */
  }

  public bool GetIncomingSource(ref GvNode /* GvNode_cstype */ node, ref GvPort /* GvPort_cstype */ port) {
    global::System.IntPtr p_node;
    unsafe { void *pp_node = &p_node;    /* GvNode_csin_pre */
    global::System.IntPtr p_port;
    unsafe { void *pp_port = &p_port;    /* GvPort_csin_pre */
    try {
      bool ret = C4dApiPINVOKE.GvPort_GetIncomingSource(swigCPtr, (global::System.IntPtr) pp_node /* GvNode_csin */, (global::System.IntPtr) pp_port /* GvPort_csin */);
      if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } finally {
        node = new GvNode(p_node, false);
        /* GvNode_csin_post */
        port = new GvPort(p_port, false);
        /* GvPort_csin_post */
    }
} /* GvPort_csin_terminator */
} /* GvNode_csin_terminator */
  }

  public GvPortIO GetIO() {
    GvPortIO ret = (GvPortIO)C4dApiPINVOKE.GvPort_GetIO(swigCPtr);
    return ret;
  }

  public void SetMainID(int id) {
    C4dApiPINVOKE.GvPort_SetMainID(swigCPtr, id);
  }

  public int GetMainID() {
    int ret = C4dApiPINVOKE.GvPort_GetMainID(swigCPtr);
    return ret;
  }

  public void SetUserID(int id) {
    C4dApiPINVOKE.GvPort_SetUserID(swigCPtr, id);
  }

  public int GetUserID() {
    int ret = C4dApiPINVOKE.GvPort_GetUserID(swigCPtr);
    return ret;
  }

  public int GetSubID() {
    int ret = C4dApiPINVOKE.GvPort_GetSubID(swigCPtr);
    return ret;
  }

  public int GetValueType() {
    int ret = C4dApiPINVOKE.GvPort_GetValueType(swigCPtr);
    return ret;
  }

  public void SetVisible(bool v) {
    C4dApiPINVOKE.GvPort_SetVisible(swigCPtr, v);
  }

  public bool GetVisible() {
    bool ret = C4dApiPINVOKE.GvPort_GetVisible(swigCPtr);
    return ret;
  }

  public void SetValid(GvRun r, bool v) {
    C4dApiPINVOKE.GvPort_SetValid(swigCPtr, GvRun.getCPtr(r), v);
  }

  public bool GetValid(GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetValid(swigCPtr, GvRun.getCPtr(r));
    return ret;
  }

  public void SetCalculated(GvRun r) {
    C4dApiPINVOKE.GvPort_SetCalculated(swigCPtr, GvRun.getCPtr(r));
  }

  public GvPort Calculate(GvNode bn, GvRun r, GvCalc c) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvPort_Calculate(swigCPtr, GvNode.getCPtr(bn), GvRun.getCPtr(r), GvCalc.getCPtr(c));
    GvPort ret = (cPtr == global::System.IntPtr.Zero) ? null : new GvPort(cPtr, false);
    return ret;
  }

  public bool SetRecalculate(GvRun r, bool force_set) {
    bool ret = C4dApiPINVOKE.GvPort_SetRecalculate__SWIG_0(swigCPtr, GvRun.getCPtr(r), force_set);
    return ret;
  }

  public bool SetRecalculate(GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetRecalculate__SWIG_1(swigCPtr, GvRun.getCPtr(r));
    return ret;
  }

  public bool GetBool(SWIGTYPE_p_Bool b, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetBool(swigCPtr, SWIGTYPE_p_Bool.getCPtr(b), GvRun.getCPtr(r));
    return ret;
  }

  public bool GetInteger(SWIGTYPE_p_Int32 i, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetInteger(swigCPtr, SWIGTYPE_p_Int32.getCPtr(i), GvRun.getCPtr(r));
    return ret;
  }

  public bool GetFloat(SWIGTYPE_p_Float f, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetFloat(swigCPtr, SWIGTYPE_p_Float.getCPtr(f), GvRun.getCPtr(r));
    return ret;
  }

  public bool GetVector(ref Fusee.Math.Core.double3 /* Vector*&_cstype */ v, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetVector(swigCPtr, ref v /* Vector*&_csin */, GvRun.getCPtr(r));
    return ret;
  }

  public bool GetNormal(ref Fusee.Math.Core.double3 /* Vector*&_cstype */ n, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetNormal(swigCPtr, ref n /* Vector*&_csin */, GvRun.getCPtr(r));
    return ret;
  }

  public bool GetMatrix(ref Fusee.Math.Core.double4x4 /* Matrix*&_cstype */ m, GvRun r) {
    double[] adbl_m;
    unsafe {adbl_m = Fusee.Math.ArrayConvert.double4x4ToArrayDoubleC4DLayout(m);    fixed (double *pdbl_m = adbl_m) {
    /* Matrix*&_csin_pre */
    try {
      bool ret = C4dApiPINVOKE.GvPort_GetMatrix(swigCPtr, (global::System.IntPtr) pdbl_m /* Matrix*&_csin */, GvRun.getCPtr(r));
      return ret;
    } finally {
        m = Fusee.Math.ArrayConvert.ArrayDoubleC4DLayoutTodouble4x4(pdbl_m);
        /* Matrix*&_csin_post */
    }
} } /* Matrix*&_csin_terminator */
  }

  public bool GetTime(BaseTime t, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetTime(swigCPtr, BaseTime.getCPtr(t), GvRun.getCPtr(r));
    return ret;
  }

  public bool GetString(SWIGTYPE_p_String s, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetString(swigCPtr, SWIGTYPE_p_String.getCPtr(s), GvRun.getCPtr(r));
    return ret;
  }

  public bool GetObject(SWIGTYPE_p_p_BaseList2D o, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetObject(swigCPtr, SWIGTYPE_p_p_BaseList2D.getCPtr(o), GvRun.getCPtr(r));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool GetData(SWIGTYPE_p_void d, int type, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetData(swigCPtr, SWIGTYPE_p_void.getCPtr(d), type, GvRun.getCPtr(r));
    return ret;
  }

  public bool GetDataInstance(SWIGTYPE_p_p_void d, int type, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetDataInstance(swigCPtr, SWIGTYPE_p_p_void.getCPtr(d), type, GvRun.getCPtr(r));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool SetBool(bool b, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetBool(swigCPtr, b, GvRun.getCPtr(r));
    return ret;
  }

  public bool SetInteger(int i, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetInteger(swigCPtr, i, GvRun.getCPtr(r));
    return ret;
  }

  public bool SetFloat(double f, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetFloat(swigCPtr, f, GvRun.getCPtr(r));
    return ret;
  }

  public bool SetVector(Fusee.Math.Core.double3 /* constVector&_cstype */ v, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetVector(swigCPtr, ref v /* constVector&_csin */, GvRun.getCPtr(r));
    return ret;
  }

  public bool SetNormal(Fusee.Math.Core.double3 /* constVector&_cstype */ n, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetNormal(swigCPtr, ref n /* constVector&_csin */, GvRun.getCPtr(r));
    return ret;
  }

  public bool SetMatrix(ref Fusee.Math.Core.double4x4 /* constMatrix&_cstype */ m, GvRun r) {
    double[] adbl_m;
    unsafe {adbl_m = Fusee.Math.ArrayConvert.double4x4ToArrayDoubleC4DLayout(m);    fixed (double *pdbl_m = adbl_m) {
    /* constMatrix&_csin_pre */
    try {
      bool ret = C4dApiPINVOKE.GvPort_SetMatrix(swigCPtr, (global::System.IntPtr) pdbl_m /* constMatrix&_csin */, GvRun.getCPtr(r));
      return ret;
    } finally {
        // NOP m = Fusee.Math.ArrayConvert.ArrayDoubleC4DLayoutTodouble4x4(pdbl_m);
        /* constMatrix&_csin_post */
    }
} } /* constMatrix&_csin_terminator */
  }

  public bool SetTime(BaseTime t, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetTime(swigCPtr, BaseTime.getCPtr(t), GvRun.getCPtr(r));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool SetString(string /* constString&_cstype */ s, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetString(swigCPtr, s, GvRun.getCPtr(r));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool SetObject(BaseList2D o, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetObject(swigCPtr, BaseList2D.getCPtr(o), GvRun.getCPtr(r));
    return ret;
  }

  public bool SetData(SWIGTYPE_p_void d, int type, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetData(swigCPtr, SWIGTYPE_p_void.getCPtr(d), type, GvRun.getCPtr(r));
    return ret;
  }

  public string /* String_cstype */ GetName(GvNode node)  {  /* <String_csout> */
      string ret = C4dApiPINVOKE.GvPort_GetName(swigCPtr, GvNode.getCPtr(node));
      return ret;
   } /* </String_csout> */ 

  public void SetName(string /* constString&_cstype */ name) {
    C4dApiPINVOKE.GvPort_SetName(swigCPtr, name);
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool CopyPortData(GvPort source, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_CopyPortData(swigCPtr, GvPort.getCPtr(source), GvRun.getCPtr(r));
    return ret;
  }

  public bool CopyRawData(SWIGTYPE_p_void source, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_CopyRawData(swigCPtr, SWIGTYPE_p_void.getCPtr(source), GvRun.getCPtr(r));
    return ret;
  }

  public bool CalculateRawData(SWIGTYPE_p_void data, SWIGTYPE_p_void dest, GvRun r, GvValueFlags calculation, double parm1) {
    bool ret = C4dApiPINVOKE.GvPort_CalculateRawData__SWIG_0(swigCPtr, SWIGTYPE_p_void.getCPtr(data), SWIGTYPE_p_void.getCPtr(dest), GvRun.getCPtr(r), (int)calculation, parm1);
    return ret;
  }

  public bool CalculateRawData(SWIGTYPE_p_void data, SWIGTYPE_p_void dest, GvRun r, GvValueFlags calculation) {
    bool ret = C4dApiPINVOKE.GvPort_CalculateRawData__SWIG_1(swigCPtr, SWIGTYPE_p_void.getCPtr(data), SWIGTYPE_p_void.getCPtr(dest), GvRun.getCPtr(r), (int)calculation);
    return ret;
  }

  public bool CalculateRawRawData(SWIGTYPE_p_void data1, SWIGTYPE_p_void data2, SWIGTYPE_p_void dest, GvRun r, GvValueFlags calculation, double parm1) {
    bool ret = C4dApiPINVOKE.GvPort_CalculateRawRawData__SWIG_0(swigCPtr, SWIGTYPE_p_void.getCPtr(data1), SWIGTYPE_p_void.getCPtr(data2), SWIGTYPE_p_void.getCPtr(dest), GvRun.getCPtr(r), (int)calculation, parm1);
    return ret;
  }

  public bool CalculateRawRawData(SWIGTYPE_p_void data1, SWIGTYPE_p_void data2, SWIGTYPE_p_void dest, GvRun r, GvValueFlags calculation) {
    bool ret = C4dApiPINVOKE.GvPort_CalculateRawRawData__SWIG_1(swigCPtr, SWIGTYPE_p_void.getCPtr(data1), SWIGTYPE_p_void.getCPtr(data2), SWIGTYPE_p_void.getCPtr(dest), GvRun.getCPtr(r), (int)calculation);
    return ret;
  }

  public bool CalculatePortData(GvPort data, SWIGTYPE_p_void dest, GvRun r, GvValueFlags calculation, double parm1) {
    bool ret = C4dApiPINVOKE.GvPort_CalculatePortData__SWIG_0(swigCPtr, GvPort.getCPtr(data), SWIGTYPE_p_void.getCPtr(dest), GvRun.getCPtr(r), (int)calculation, parm1);
    return ret;
  }

  public bool CalculatePortData(GvPort data, SWIGTYPE_p_void dest, GvRun r, GvValueFlags calculation) {
    bool ret = C4dApiPINVOKE.GvPort_CalculatePortData__SWIG_1(swigCPtr, GvPort.getCPtr(data), SWIGTYPE_p_void.getCPtr(dest), GvRun.getCPtr(r), (int)calculation);
    return ret;
  }

  public bool CalculateRawDataRev(SWIGTYPE_p_void data, SWIGTYPE_p_void dest, GvRun r, GvValueFlags calculation, double parm1) {
    bool ret = C4dApiPINVOKE.GvPort_CalculateRawDataRev__SWIG_0(swigCPtr, SWIGTYPE_p_void.getCPtr(data), SWIGTYPE_p_void.getCPtr(dest), GvRun.getCPtr(r), (int)calculation, parm1);
    return ret;
  }

  public bool CalculateRawDataRev(SWIGTYPE_p_void data, SWIGTYPE_p_void dest, GvRun r, GvValueFlags calculation) {
    bool ret = C4dApiPINVOKE.GvPort_CalculateRawDataRev__SWIG_1(swigCPtr, SWIGTYPE_p_void.getCPtr(data), SWIGTYPE_p_void.getCPtr(dest), GvRun.getCPtr(r), (int)calculation);
    return ret;
  }

  public bool GetTag(SWIGTYPE_p_p_BaseList2D t, GvRun r, SWIGTYPE_p_Int32 index) {
    bool ret = C4dApiPINVOKE.GvPort_GetTag__SWIG_0(swigCPtr, SWIGTYPE_p_p_BaseList2D.getCPtr(t), GvRun.getCPtr(r), SWIGTYPE_p_Int32.getCPtr(index));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool GetTag(SWIGTYPE_p_p_BaseList2D t, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetTag__SWIG_1(swigCPtr, SWIGTYPE_p_p_BaseList2D.getCPtr(t), GvRun.getCPtr(r));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool SetTag(BaseList2D t, GvRun r, int index) {
    bool ret = C4dApiPINVOKE.GvPort_SetTag__SWIG_0(swigCPtr, BaseList2D.getCPtr(t), GvRun.getCPtr(r), index);
    return ret;
  }

  public bool SetTag(BaseList2D t, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetTag__SWIG_1(swigCPtr, BaseList2D.getCPtr(t), GvRun.getCPtr(r));
    return ret;
  }

  public bool GetMaterial(SWIGTYPE_p_p_BaseList2D m, GvRun r, SWIGTYPE_p_Int32 index) {
    bool ret = C4dApiPINVOKE.GvPort_GetMaterial__SWIG_0(swigCPtr, SWIGTYPE_p_p_BaseList2D.getCPtr(m), GvRun.getCPtr(r), SWIGTYPE_p_Int32.getCPtr(index));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool GetMaterial(SWIGTYPE_p_p_BaseList2D m, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetMaterial__SWIG_1(swigCPtr, SWIGTYPE_p_p_BaseList2D.getCPtr(m), GvRun.getCPtr(r));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool SetMaterial(BaseList2D m, GvRun r, int index) {
    bool ret = C4dApiPINVOKE.GvPort_SetMaterial__SWIG_0(swigCPtr, BaseList2D.getCPtr(m), GvRun.getCPtr(r), index);
    return ret;
  }

  public bool SetMaterial(BaseList2D m, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetMaterial__SWIG_1(swigCPtr, BaseList2D.getCPtr(m), GvRun.getCPtr(r));
    return ret;
  }

  public bool GetInstance(SWIGTYPE_p_p_BaseList2D i, GvRun r, SWIGTYPE_p_Int32 index) {
    bool ret = C4dApiPINVOKE.GvPort_GetInstance__SWIG_0(swigCPtr, SWIGTYPE_p_p_BaseList2D.getCPtr(i), GvRun.getCPtr(r), SWIGTYPE_p_Int32.getCPtr(index));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool GetInstance(SWIGTYPE_p_p_BaseList2D i, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetInstance__SWIG_1(swigCPtr, SWIGTYPE_p_p_BaseList2D.getCPtr(i), GvRun.getCPtr(r));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool SetInstance(BaseList2D i, GvRun r, int index) {
    bool ret = C4dApiPINVOKE.GvPort_SetInstance__SWIG_0(swigCPtr, BaseList2D.getCPtr(i), GvRun.getCPtr(r), index);
    return ret;
  }

  public bool SetInstance(BaseList2D i, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetInstance__SWIG_1(swigCPtr, BaseList2D.getCPtr(i), GvRun.getCPtr(r));
    return ret;
  }

  public bool GetObjectWithIndex(SWIGTYPE_p_p_BaseList2D o, GvRun r, SWIGTYPE_p_Int32 index) {
    bool ret = C4dApiPINVOKE.GvPort_GetObjectWithIndex__SWIG_0(swigCPtr, SWIGTYPE_p_p_BaseList2D.getCPtr(o), GvRun.getCPtr(r), SWIGTYPE_p_Int32.getCPtr(index));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool GetObjectWithIndex(SWIGTYPE_p_p_BaseList2D o, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_GetObjectWithIndex__SWIG_1(swigCPtr, SWIGTYPE_p_p_BaseList2D.getCPtr(o), GvRun.getCPtr(r));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool SetObjectWithIndex(BaseList2D o, GvRun r, int index) {
    bool ret = C4dApiPINVOKE.GvPort_SetObjectWithIndex__SWIG_0(swigCPtr, BaseList2D.getCPtr(o), GvRun.getCPtr(r), index);
    return ret;
  }

  public bool SetObjectWithIndex(BaseList2D o, GvRun r) {
    bool ret = C4dApiPINVOKE.GvPort_SetObjectWithIndex__SWIG_1(swigCPtr, BaseList2D.getCPtr(o), GvRun.getCPtr(r));
    return ret;
  }

  public GvDestination GetOutgoing(int index) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.GvPort_GetOutgoing(swigCPtr, index);
    GvDestination ret = (cPtr == global::System.IntPtr.Zero) ? null : new GvDestination(cPtr, false);
    return ret;
  }

}

}
