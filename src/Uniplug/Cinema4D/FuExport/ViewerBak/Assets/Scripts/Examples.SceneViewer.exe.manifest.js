// Generated by JSIL v0.8.2 build 18792. See http://jsil.org/ for more information. 

'use strict';
var $asm00 = JSIL.GetAssembly("Examples.SceneViewer, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null");
var $asm01 = JSIL.GetAssembly("Fusee.Engine.Common, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null");
var $asm02 = JSIL.GetAssembly("Fusee.Engine.Core, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null");
var $asm03 = JSIL.GetAssembly("Fusee.Engine.SimpleScene, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm04 = JSIL.GetAssembly("Fusee.KeyFrameAnimation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm05 = JSIL.GetAssembly("Fusee.Math.Core, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null");
var $asm06 = JSIL.GetAssembly("Fusee.SerializationContainer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm07 = JSIL.GetAssembly("Fusee.SerializationSerializer, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm08 = JSIL.GetAssembly("Fusee.Xirkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm09 = JSIL.GetAssembly("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
var $asm0A = JSIL.GetAssembly("protobuf-net, Version=2.0.0.668, Culture=neutral, PublicKeyToken=257b51d87d2e4d67");
var $asm0B = JSIL.GetAssembly("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
var $asm0C = JSIL.GetAssembly("System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
var $asm0D = JSIL.GetAssembly("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
var $asm0E = JSIL.GetAssembly("System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
var $asm0F = JSIL.GetAssembly("System.Numerics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
var $asm10 = JSIL.GetAssembly("System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
var $asm11 = JSIL.GetAssembly("System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
var $asm12 = JSIL.GetAssembly("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
var $asm13 = JSIL.GetAssembly("System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
var $asm14 = JSIL.GetAssembly("System.Data.SqlXml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");

if (typeof (contentManifest) !== "object") { JSIL.GlobalNamespace.contentManifest = {}; };
contentManifest["Examples.SceneViewer.exe"] = [
    ["Script", "Examples.SceneViewer, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 25124 }],
    ["Script", "Fusee.SerializationContainer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 38447 }],
    ["Script", "Fusee.KeyFrameAnimation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 38424 }],
    ["Script", "Fusee.Engine.SimpleScene, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 226750 }],
    ["Script", "Fusee.Engine.Common, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 176771 }],
    ["Script", "Fusee.SerializationSerializer, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 156212 }],
    ["Script", "protobuf-net, Version=2.0.0.668, Culture=neutral, PublicKeyToken=257b51d87d2e4d67.js", { "sizeBytes": 411611 }],
    ["Script", "System.Numerics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089.js", { "sizeBytes": 172969 }],
    ["Script", "Fusee.Xirkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 137705 }],
    ["Script", "Fusee.Math.Core, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 527726 }],
    ["Script", "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a.js", { "sizeBytes": 1272668 }],
    ["Script", "System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a.js", { "sizeBytes": 648444 }],
    ["Script", "Fusee.Engine.Core, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 730892 }],
    ["Script", "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089.js", { "sizeBytes": 3023201 }],
    ["Script", "System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089.js", { "sizeBytes": 4197790 }],
    ["Script", "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089.js", { "sizeBytes": 9353291 }],
    ["Script", "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089.js", { "sizeBytes": 6334575 }],
];