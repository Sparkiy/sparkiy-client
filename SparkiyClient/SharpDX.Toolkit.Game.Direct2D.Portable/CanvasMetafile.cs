using System;
using System.Linq;
using System.Reflection;
using SharpDX.Direct2D1;
using SharpDX.IO;
using SharpDX.WIC;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub {
    public class CanvasMetafile : CanvasDrawing {
        private readonly string _fileName;
        private readonly GdiMetafile _gdiMetafile;

        public CanvasMetafile(string fileName, Vector2? targetOffset = null) {
            throw new NotImplementedException();
            //if (fileName == null) throw new ArgumentNullException("fileName");
            //_fileName = fileName;
            //TargetOffset = targetOffset;
            //using (var imagingFactory = new ImagingFactory())
            //using (var nativeFileStream = new NativeFileStream(fileName, NativeFileMode.Open, NativeFileAccess.Read))
            //using (var stream = new WICStream(imagingFactory, nativeFileStream)) {
            //    //  _gdiMetafile = new GdiMetafile(stream.NativePointer);

            //    TypeInfo typeInfo = nativeFileStream.GetType().GetTypeInfo();
            //    FieldInfo[] fieldInfos = typeInfo.DeclaredFields.ToArray();
            //    FieldInfo fieldInfo = fieldInfos[3];
            //    object value = fieldInfo.GetValue(nativeFileStream);
            //    var intPtr = (IntPtr) value;
            //    _gdiMetafile = new GdiMetafile(intPtr);
            //}
        }

        public string FileName {
            get { return _fileName; }
        }

        public Vector2? TargetOffset { get; set; }


        internal override void DoWork(DeviceContext context) {
            context.DrawGdiMetafile(_gdiMetafile, TargetOffset);
        }

        internal override bool CanExecute() {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return string.Format("TargetOffset: {0}", TargetOffset);
        }
    }
}