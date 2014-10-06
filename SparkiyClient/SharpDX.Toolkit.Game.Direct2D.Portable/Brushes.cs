using SharpDX.Direct2D1;

namespace SharpDX.Toolkit.Direct2D.Test.CanvasStub
{
    public static class Brushes
    {
        private static DeviceContext _deviceContext;

        public static void Initialize(DeviceContext deviceContext)
        {
            _deviceContext = deviceContext;
        }

        #region Lazy initialization

        private static SolidColorBrush _zero;
        private static SolidColorBrush _transparent;
        private static SolidColorBrush _aliceblue;
        private static SolidColorBrush _antiquewhite;
        private static SolidColorBrush _aqua;
        private static SolidColorBrush _aquamarine;
        private static SolidColorBrush _azure;
        private static SolidColorBrush _beige;
        private static SolidColorBrush _bisque;
        private static SolidColorBrush _black;
        private static SolidColorBrush _blanchedalmond;
        private static SolidColorBrush _blue;
        private static SolidColorBrush _blueviolet;
        private static SolidColorBrush _brown;
        private static SolidColorBrush _burlywood;
        private static SolidColorBrush _cadetblue;
        private static SolidColorBrush _chartreuse;
        private static SolidColorBrush _chocolate;
        private static SolidColorBrush _coral;
        private static SolidColorBrush _cornflowerblue;
        private static SolidColorBrush _cornsilk;
        private static SolidColorBrush _crimson;
        private static SolidColorBrush _cyan;
        private static SolidColorBrush _darkblue;
        private static SolidColorBrush _darkcyan;
        private static SolidColorBrush _darkgoldenrod;
        private static SolidColorBrush _darkgray;
        private static SolidColorBrush _darkgreen;
        private static SolidColorBrush _darkkhaki;
        private static SolidColorBrush _darkmagenta;
        private static SolidColorBrush _darkolivegreen;
        private static SolidColorBrush _darkorange;
        private static SolidColorBrush _darkorchid;
        private static SolidColorBrush _darkred;
        private static SolidColorBrush _darksalmon;
        private static SolidColorBrush _darkseagreen;
        private static SolidColorBrush _darkslateblue;
        private static SolidColorBrush _darkslategray;
        private static SolidColorBrush _darkturquoise;
        private static SolidColorBrush _darkviolet;
        private static SolidColorBrush _deeppink;
        private static SolidColorBrush _deepskyblue;
        private static SolidColorBrush _dimgray;
        private static SolidColorBrush _dodgerblue;
        private static SolidColorBrush _firebrick;
        private static SolidColorBrush _floralwhite;
        private static SolidColorBrush _forestgreen;
        private static SolidColorBrush _fuchsia;
        private static SolidColorBrush _gainsboro;
        private static SolidColorBrush _ghostwhite;
        private static SolidColorBrush _gold;
        private static SolidColorBrush _goldenrod;
        private static SolidColorBrush _gray;
        private static SolidColorBrush _green;
        private static SolidColorBrush _greenyellow;
        private static SolidColorBrush _honeydew;
        private static SolidColorBrush _hotpink;
        private static SolidColorBrush _indianred;
        private static SolidColorBrush _indigo;
        private static SolidColorBrush _ivory;
        private static SolidColorBrush _khaki;
        private static SolidColorBrush _lavender;
        private static SolidColorBrush _lavenderblush;
        private static SolidColorBrush _lawngreen;
        private static SolidColorBrush _lemonchiffon;
        private static SolidColorBrush _lightblue;
        private static SolidColorBrush _lightcoral;
        private static SolidColorBrush _lightcyan;
        private static SolidColorBrush _lightgoldenrodyellow;
        private static SolidColorBrush _lightgray;
        private static SolidColorBrush _lightgreen;
        private static SolidColorBrush _lightpink;
        private static SolidColorBrush _lightsalmon;
        private static SolidColorBrush _lightseagreen;
        private static SolidColorBrush _lightskyblue;
        private static SolidColorBrush _lightslategray;
        private static SolidColorBrush _lightsteelblue;
        private static SolidColorBrush _lightyellow;
        private static SolidColorBrush _lime;
        private static SolidColorBrush _limegreen;
        private static SolidColorBrush _linen;
        private static SolidColorBrush _magenta;
        private static SolidColorBrush _maroon;
        private static SolidColorBrush _mediumaquamarine;
        private static SolidColorBrush _mediumblue;
        private static SolidColorBrush _mediumorchid;
        private static SolidColorBrush _mediumpurple;
        private static SolidColorBrush _mediumseagreen;
        private static SolidColorBrush _mediumslateblue;
        private static SolidColorBrush _mediumspringgreen;
        private static SolidColorBrush _mediumturquoise;
        private static SolidColorBrush _mediumvioletred;
        private static SolidColorBrush _midnightblue;
        private static SolidColorBrush _mintcream;
        private static SolidColorBrush _mistyrose;
        private static SolidColorBrush _moccasin;
        private static SolidColorBrush _navajowhite;
        private static SolidColorBrush _navy;
        private static SolidColorBrush _oldlace;
        private static SolidColorBrush _olive;
        private static SolidColorBrush _olivedrab;
        private static SolidColorBrush _orange;
        private static SolidColorBrush _orangered;
        private static SolidColorBrush _orchid;
        private static SolidColorBrush _palegoldenrod;
        private static SolidColorBrush _palegreen;
        private static SolidColorBrush _paleturquoise;
        private static SolidColorBrush _palevioletred;
        private static SolidColorBrush _papayawhip;
        private static SolidColorBrush _peachpuff;
        private static SolidColorBrush _peru;
        private static SolidColorBrush _pink;
        private static SolidColorBrush _plum;
        private static SolidColorBrush _powderblue;
        private static SolidColorBrush _purple;
        private static SolidColorBrush _red;
        private static SolidColorBrush _rosybrown;
        private static SolidColorBrush _royalblue;
        private static SolidColorBrush _saddlebrown;
        private static SolidColorBrush _salmon;
        private static SolidColorBrush _sandybrown;
        private static SolidColorBrush _seagreen;
        private static SolidColorBrush _seashell;
        private static SolidColorBrush _sienna;
        private static SolidColorBrush _silver;
        private static SolidColorBrush _skyblue;
        private static SolidColorBrush _slateblue;
        private static SolidColorBrush _slategray;
        private static SolidColorBrush _snow;
        private static SolidColorBrush _springgreen;
        private static SolidColorBrush _steelblue;
        private static SolidColorBrush _tan;
        private static SolidColorBrush _teal;
        private static SolidColorBrush _thistle;
        private static SolidColorBrush _tomato;
        private static SolidColorBrush _turquoise;
        private static SolidColorBrush _violet;
        private static SolidColorBrush _wheat;
        private static SolidColorBrush _white;
        private static SolidColorBrush _whitesmoke;
        private static SolidColorBrush _yellow;
        private static SolidColorBrush _yellowgreen;

        public static SolidColorBrush Zero
        {
            get
            {
                if (_zero == null)
                {
                    _zero = new SolidColorBrush(_deviceContext, Color.Zero);
                }
                return _zero;
            }
        }

        public static SolidColorBrush Transparent
        {
            get
            {
                if (_transparent == null)
                {
                    _transparent = new SolidColorBrush(_deviceContext, Color.Transparent);
                }
                return _transparent;
            }
        }

        public static SolidColorBrush AliceBlue
        {
            get
            {
                if (_aliceblue == null)
                {
                    _aliceblue = new SolidColorBrush(_deviceContext, Color.AliceBlue);
                }
                return _aliceblue;
            }
        }

        public static SolidColorBrush AntiqueWhite
        {
            get
            {
                if (_antiquewhite == null)
                {
                    _antiquewhite = new SolidColorBrush(_deviceContext, Color.AntiqueWhite);
                }
                return _antiquewhite;
            }
        }

        public static SolidColorBrush Aqua
        {
            get
            {
                if (_aqua == null)
                {
                    _aqua = new SolidColorBrush(_deviceContext, Color.Aqua);
                }
                return _aqua;
            }
        }

        public static SolidColorBrush Aquamarine
        {
            get
            {
                if (_aquamarine == null)
                {
                    _aquamarine = new SolidColorBrush(_deviceContext, Color.Aquamarine);
                }
                return _aquamarine;
            }
        }

        public static SolidColorBrush Azure
        {
            get
            {
                if (_azure == null)
                {
                    _azure = new SolidColorBrush(_deviceContext, Color.Azure);
                }
                return _azure;
            }
        }

        public static SolidColorBrush Beige
        {
            get
            {
                if (_beige == null)
                {
                    _beige = new SolidColorBrush(_deviceContext, Color.Beige);
                }
                return _beige;
            }
        }

        public static SolidColorBrush Bisque
        {
            get
            {
                if (_bisque == null)
                {
                    _bisque = new SolidColorBrush(_deviceContext, Color.Bisque);
                }
                return _bisque;
            }
        }

        public static SolidColorBrush Black
        {
            get
            {
                if (_black == null)
                {
                    _black = new SolidColorBrush(_deviceContext, Color.Black);
                }
                return _black;
            }
        }

        public static SolidColorBrush BlanchedAlmond
        {
            get
            {
                if (_blanchedalmond == null)
                {
                    _blanchedalmond = new SolidColorBrush(_deviceContext, Color.BlanchedAlmond);
                }
                return _blanchedalmond;
            }
        }

        public static SolidColorBrush Blue
        {
            get
            {
                if (_blue == null)
                {
                    _blue = new SolidColorBrush(_deviceContext, Color.Blue);
                }
                return _blue;
            }
        }

        public static SolidColorBrush BlueViolet
        {
            get
            {
                if (_blueviolet == null)
                {
                    _blueviolet = new SolidColorBrush(_deviceContext, Color.BlueViolet);
                }
                return _blueviolet;
            }
        }

        public static SolidColorBrush Brown
        {
            get
            {
                if (_brown == null)
                {
                    _brown = new SolidColorBrush(_deviceContext, Color.Brown);
                }
                return _brown;
            }
        }

        public static SolidColorBrush BurlyWood
        {
            get
            {
                if (_burlywood == null)
                {
                    _burlywood = new SolidColorBrush(_deviceContext, Color.BurlyWood);
                }
                return _burlywood;
            }
        }

        public static SolidColorBrush CadetBlue
        {
            get
            {
                if (_cadetblue == null)
                {
                    _cadetblue = new SolidColorBrush(_deviceContext, Color.CadetBlue);
                }
                return _cadetblue;
            }
        }

        public static SolidColorBrush Chartreuse
        {
            get
            {
                if (_chartreuse == null)
                {
                    _chartreuse = new SolidColorBrush(_deviceContext, Color.Chartreuse);
                }
                return _chartreuse;
            }
        }

        public static SolidColorBrush Chocolate
        {
            get
            {
                if (_chocolate == null)
                {
                    _chocolate = new SolidColorBrush(_deviceContext, Color.Chocolate);
                }
                return _chocolate;
            }
        }

        public static SolidColorBrush Coral
        {
            get
            {
                if (_coral == null)
                {
                    _coral = new SolidColorBrush(_deviceContext, Color.Coral);
                }
                return _coral;
            }
        }

        public static SolidColorBrush CornflowerBlue
        {
            get
            {
                if (_cornflowerblue == null)
                {
                    _cornflowerblue = new SolidColorBrush(_deviceContext, Color.CornflowerBlue);
                }
                return _cornflowerblue;
            }
        }

        public static SolidColorBrush Cornsilk
        {
            get
            {
                if (_cornsilk == null)
                {
                    _cornsilk = new SolidColorBrush(_deviceContext, Color.Cornsilk);
                }
                return _cornsilk;
            }
        }

        public static SolidColorBrush Crimson
        {
            get
            {
                if (_crimson == null)
                {
                    _crimson = new SolidColorBrush(_deviceContext, Color.Crimson);
                }
                return _crimson;
            }
        }

        public static SolidColorBrush Cyan
        {
            get
            {
                if (_cyan == null)
                {
                    _cyan = new SolidColorBrush(_deviceContext, Color.Cyan);
                }
                return _cyan;
            }
        }

        public static SolidColorBrush DarkBlue
        {
            get
            {
                if (_darkblue == null)
                {
                    _darkblue = new SolidColorBrush(_deviceContext, Color.DarkBlue);
                }
                return _darkblue;
            }
        }

        public static SolidColorBrush DarkCyan
        {
            get
            {
                if (_darkcyan == null)
                {
                    _darkcyan = new SolidColorBrush(_deviceContext, Color.DarkCyan);
                }
                return _darkcyan;
            }
        }

        public static SolidColorBrush DarkGoldenrod
        {
            get
            {
                if (_darkgoldenrod == null)
                {
                    _darkgoldenrod = new SolidColorBrush(_deviceContext, Color.DarkGoldenrod);
                }
                return _darkgoldenrod;
            }
        }

        public static SolidColorBrush DarkGray
        {
            get
            {
                if (_darkgray == null)
                {
                    _darkgray = new SolidColorBrush(_deviceContext, Color.DarkGray);
                }
                return _darkgray;
            }
        }

        public static SolidColorBrush DarkGreen
        {
            get
            {
                if (_darkgreen == null)
                {
                    _darkgreen = new SolidColorBrush(_deviceContext, Color.DarkGreen);
                }
                return _darkgreen;
            }
        }

        public static SolidColorBrush DarkKhaki
        {
            get
            {
                if (_darkkhaki == null)
                {
                    _darkkhaki = new SolidColorBrush(_deviceContext, Color.DarkKhaki);
                }
                return _darkkhaki;
            }
        }

        public static SolidColorBrush DarkMagenta
        {
            get
            {
                if (_darkmagenta == null)
                {
                    _darkmagenta = new SolidColorBrush(_deviceContext, Color.DarkMagenta);
                }
                return _darkmagenta;
            }
        }

        public static SolidColorBrush DarkOliveGreen
        {
            get
            {
                if (_darkolivegreen == null)
                {
                    _darkolivegreen = new SolidColorBrush(_deviceContext, Color.DarkOliveGreen);
                }
                return _darkolivegreen;
            }
        }

        public static SolidColorBrush DarkOrange
        {
            get
            {
                if (_darkorange == null)
                {
                    _darkorange = new SolidColorBrush(_deviceContext, Color.DarkOrange);
                }
                return _darkorange;
            }
        }

        public static SolidColorBrush DarkOrchid
        {
            get
            {
                if (_darkorchid == null)
                {
                    _darkorchid = new SolidColorBrush(_deviceContext, Color.DarkOrchid);
                }
                return _darkorchid;
            }
        }

        public static SolidColorBrush DarkRed
        {
            get
            {
                if (_darkred == null)
                {
                    _darkred = new SolidColorBrush(_deviceContext, Color.DarkRed);
                }
                return _darkred;
            }
        }

        public static SolidColorBrush DarkSalmon
        {
            get
            {
                if (_darksalmon == null)
                {
                    _darksalmon = new SolidColorBrush(_deviceContext, Color.DarkSalmon);
                }
                return _darksalmon;
            }
        }

        public static SolidColorBrush DarkSeaGreen
        {
            get
            {
                if (_darkseagreen == null)
                {
                    _darkseagreen = new SolidColorBrush(_deviceContext, Color.DarkSeaGreen);
                }
                return _darkseagreen;
            }
        }

        public static SolidColorBrush DarkSlateBlue
        {
            get
            {
                if (_darkslateblue == null)
                {
                    _darkslateblue = new SolidColorBrush(_deviceContext, Color.DarkSlateBlue);
                }
                return _darkslateblue;
            }
        }

        public static SolidColorBrush DarkSlateGray
        {
            get
            {
                if (_darkslategray == null)
                {
                    _darkslategray = new SolidColorBrush(_deviceContext, Color.DarkSlateGray);
                }
                return _darkslategray;
            }
        }

        public static SolidColorBrush DarkTurquoise
        {
            get
            {
                if (_darkturquoise == null)
                {
                    _darkturquoise = new SolidColorBrush(_deviceContext, Color.DarkTurquoise);
                }
                return _darkturquoise;
            }
        }

        public static SolidColorBrush DarkViolet
        {
            get
            {
                if (_darkviolet == null)
                {
                    _darkviolet = new SolidColorBrush(_deviceContext, Color.DarkViolet);
                }
                return _darkviolet;
            }
        }

        public static SolidColorBrush DeepPink
        {
            get
            {
                if (_deeppink == null)
                {
                    _deeppink = new SolidColorBrush(_deviceContext, Color.DeepPink);
                }
                return _deeppink;
            }
        }

        public static SolidColorBrush DeepSkyBlue
        {
            get
            {
                if (_deepskyblue == null)
                {
                    _deepskyblue = new SolidColorBrush(_deviceContext, Color.DeepSkyBlue);
                }
                return _deepskyblue;
            }
        }

        public static SolidColorBrush DimGray
        {
            get
            {
                if (_dimgray == null)
                {
                    _dimgray = new SolidColorBrush(_deviceContext, Color.DimGray);
                }
                return _dimgray;
            }
        }

        public static SolidColorBrush DodgerBlue
        {
            get
            {
                if (_dodgerblue == null)
                {
                    _dodgerblue = new SolidColorBrush(_deviceContext, Color.DodgerBlue);
                }
                return _dodgerblue;
            }
        }

        public static SolidColorBrush Firebrick
        {
            get
            {
                if (_firebrick == null)
                {
                    _firebrick = new SolidColorBrush(_deviceContext, Color.Firebrick);
                }
                return _firebrick;
            }
        }

        public static SolidColorBrush FloralWhite
        {
            get
            {
                if (_floralwhite == null)
                {
                    _floralwhite = new SolidColorBrush(_deviceContext, Color.FloralWhite);
                }
                return _floralwhite;
            }
        }

        public static SolidColorBrush ForestGreen
        {
            get
            {
                if (_forestgreen == null)
                {
                    _forestgreen = new SolidColorBrush(_deviceContext, Color.ForestGreen);
                }
                return _forestgreen;
            }
        }

        public static SolidColorBrush Fuchsia
        {
            get
            {
                if (_fuchsia == null)
                {
                    _fuchsia = new SolidColorBrush(_deviceContext, Color.Fuchsia);
                }
                return _fuchsia;
            }
        }

        public static SolidColorBrush Gainsboro
        {
            get
            {
                if (_gainsboro == null)
                {
                    _gainsboro = new SolidColorBrush(_deviceContext, Color.Gainsboro);
                }
                return _gainsboro;
            }
        }

        public static SolidColorBrush GhostWhite
        {
            get
            {
                if (_ghostwhite == null)
                {
                    _ghostwhite = new SolidColorBrush(_deviceContext, Color.GhostWhite);
                }
                return _ghostwhite;
            }
        }

        public static SolidColorBrush Gold
        {
            get
            {
                if (_gold == null)
                {
                    _gold = new SolidColorBrush(_deviceContext, Color.Gold);
                }
                return _gold;
            }
        }

        public static SolidColorBrush Goldenrod
        {
            get
            {
                if (_goldenrod == null)
                {
                    _goldenrod = new SolidColorBrush(_deviceContext, Color.Goldenrod);
                }
                return _goldenrod;
            }
        }

        public static SolidColorBrush Gray
        {
            get
            {
                if (_gray == null)
                {
                    _gray = new SolidColorBrush(_deviceContext, Color.Gray);
                }
                return _gray;
            }
        }

        public static SolidColorBrush Green
        {
            get
            {
                if (_green == null)
                {
                    _green = new SolidColorBrush(_deviceContext, Color.Green);
                }
                return _green;
            }
        }

        public static SolidColorBrush GreenYellow
        {
            get
            {
                if (_greenyellow == null)
                {
                    _greenyellow = new SolidColorBrush(_deviceContext, Color.GreenYellow);
                }
                return _greenyellow;
            }
        }

        public static SolidColorBrush Honeydew
        {
            get
            {
                if (_honeydew == null)
                {
                    _honeydew = new SolidColorBrush(_deviceContext, Color.Honeydew);
                }
                return _honeydew;
            }
        }

        public static SolidColorBrush HotPink
        {
            get
            {
                if (_hotpink == null)
                {
                    _hotpink = new SolidColorBrush(_deviceContext, Color.HotPink);
                }
                return _hotpink;
            }
        }

        public static SolidColorBrush IndianRed
        {
            get
            {
                if (_indianred == null)
                {
                    _indianred = new SolidColorBrush(_deviceContext, Color.IndianRed);
                }
                return _indianred;
            }
        }

        public static SolidColorBrush Indigo
        {
            get
            {
                if (_indigo == null)
                {
                    _indigo = new SolidColorBrush(_deviceContext, Color.Indigo);
                }
                return _indigo;
            }
        }

        public static SolidColorBrush Ivory
        {
            get
            {
                if (_ivory == null)
                {
                    _ivory = new SolidColorBrush(_deviceContext, Color.Ivory);
                }
                return _ivory;
            }
        }

        public static SolidColorBrush Khaki
        {
            get
            {
                if (_khaki == null)
                {
                    _khaki = new SolidColorBrush(_deviceContext, Color.Khaki);
                }
                return _khaki;
            }
        }

        public static SolidColorBrush Lavender
        {
            get
            {
                if (_lavender == null)
                {
                    _lavender = new SolidColorBrush(_deviceContext, Color.Lavender);
                }
                return _lavender;
            }
        }

        public static SolidColorBrush LavenderBlush
        {
            get
            {
                if (_lavenderblush == null)
                {
                    _lavenderblush = new SolidColorBrush(_deviceContext, Color.LavenderBlush);
                }
                return _lavenderblush;
            }
        }

        public static SolidColorBrush LawnGreen
        {
            get
            {
                if (_lawngreen == null)
                {
                    _lawngreen = new SolidColorBrush(_deviceContext, Color.LawnGreen);
                }
                return _lawngreen;
            }
        }

        public static SolidColorBrush LemonChiffon
        {
            get
            {
                if (_lemonchiffon == null)
                {
                    _lemonchiffon = new SolidColorBrush(_deviceContext, Color.LemonChiffon);
                }
                return _lemonchiffon;
            }
        }

        public static SolidColorBrush LightBlue
        {
            get
            {
                if (_lightblue == null)
                {
                    _lightblue = new SolidColorBrush(_deviceContext, Color.LightBlue);
                }
                return _lightblue;
            }
        }

        public static SolidColorBrush LightCoral
        {
            get
            {
                if (_lightcoral == null)
                {
                    _lightcoral = new SolidColorBrush(_deviceContext, Color.LightCoral);
                }
                return _lightcoral;
            }
        }

        public static SolidColorBrush LightCyan
        {
            get
            {
                if (_lightcyan == null)
                {
                    _lightcyan = new SolidColorBrush(_deviceContext, Color.LightCyan);
                }
                return _lightcyan;
            }
        }

        public static SolidColorBrush LightGoldenrodYellow
        {
            get
            {
                if (_lightgoldenrodyellow == null)
                {
                    _lightgoldenrodyellow = new SolidColorBrush(_deviceContext, Color.LightGoldenrodYellow);
                }
                return _lightgoldenrodyellow;
            }
        }

        public static SolidColorBrush LightGray
        {
            get
            {
                if (_lightgray == null)
                {
                    _lightgray = new SolidColorBrush(_deviceContext, Color.LightGray);
                }
                return _lightgray;
            }
        }

        public static SolidColorBrush LightGreen
        {
            get
            {
                if (_lightgreen == null)
                {
                    _lightgreen = new SolidColorBrush(_deviceContext, Color.LightGreen);
                }
                return _lightgreen;
            }
        }

        public static SolidColorBrush LightPink
        {
            get
            {
                if (_lightpink == null)
                {
                    _lightpink = new SolidColorBrush(_deviceContext, Color.LightPink);
                }
                return _lightpink;
            }
        }

        public static SolidColorBrush LightSalmon
        {
            get
            {
                if (_lightsalmon == null)
                {
                    _lightsalmon = new SolidColorBrush(_deviceContext, Color.LightSalmon);
                }
                return _lightsalmon;
            }
        }

        public static SolidColorBrush LightSeaGreen
        {
            get
            {
                if (_lightseagreen == null)
                {
                    _lightseagreen = new SolidColorBrush(_deviceContext, Color.LightSeaGreen);
                }
                return _lightseagreen;
            }
        }

        public static SolidColorBrush LightSkyBlue
        {
            get
            {
                if (_lightskyblue == null)
                {
                    _lightskyblue = new SolidColorBrush(_deviceContext, Color.LightSkyBlue);
                }
                return _lightskyblue;
            }
        }

        public static SolidColorBrush LightSlateGray
        {
            get
            {
                if (_lightslategray == null)
                {
                    _lightslategray = new SolidColorBrush(_deviceContext, Color.LightSlateGray);
                }
                return _lightslategray;
            }
        }

        public static SolidColorBrush LightSteelBlue
        {
            get
            {
                if (_lightsteelblue == null)
                {
                    _lightsteelblue = new SolidColorBrush(_deviceContext, Color.LightSteelBlue);
                }
                return _lightsteelblue;
            }
        }

        public static SolidColorBrush LightYellow
        {
            get
            {
                if (_lightyellow == null)
                {
                    _lightyellow = new SolidColorBrush(_deviceContext, Color.LightYellow);
                }
                return _lightyellow;
            }
        }

        public static SolidColorBrush Lime
        {
            get
            {
                if (_lime == null)
                {
                    _lime = new SolidColorBrush(_deviceContext, Color.Lime);
                }
                return _lime;
            }
        }

        public static SolidColorBrush LimeGreen
        {
            get
            {
                if (_limegreen == null)
                {
                    _limegreen = new SolidColorBrush(_deviceContext, Color.LimeGreen);
                }
                return _limegreen;
            }
        }

        public static SolidColorBrush Linen
        {
            get
            {
                if (_linen == null)
                {
                    _linen = new SolidColorBrush(_deviceContext, Color.Linen);
                }
                return _linen;
            }
        }

        public static SolidColorBrush Magenta
        {
            get
            {
                if (_magenta == null)
                {
                    _magenta = new SolidColorBrush(_deviceContext, Color.Magenta);
                }
                return _magenta;
            }
        }

        public static SolidColorBrush Maroon
        {
            get
            {
                if (_maroon == null)
                {
                    _maroon = new SolidColorBrush(_deviceContext, Color.Maroon);
                }
                return _maroon;
            }
        }

        public static SolidColorBrush MediumAquamarine
        {
            get
            {
                if (_mediumaquamarine == null)
                {
                    _mediumaquamarine = new SolidColorBrush(_deviceContext, Color.MediumAquamarine);
                }
                return _mediumaquamarine;
            }
        }

        public static SolidColorBrush MediumBlue
        {
            get
            {
                if (_mediumblue == null)
                {
                    _mediumblue = new SolidColorBrush(_deviceContext, Color.MediumBlue);
                }
                return _mediumblue;
            }
        }

        public static SolidColorBrush MediumOrchid
        {
            get
            {
                if (_mediumorchid == null)
                {
                    _mediumorchid = new SolidColorBrush(_deviceContext, Color.MediumOrchid);
                }
                return _mediumorchid;
            }
        }

        public static SolidColorBrush MediumPurple
        {
            get
            {
                if (_mediumpurple == null)
                {
                    _mediumpurple = new SolidColorBrush(_deviceContext, Color.MediumPurple);
                }
                return _mediumpurple;
            }
        }

        public static SolidColorBrush MediumSeaGreen
        {
            get
            {
                if (_mediumseagreen == null)
                {
                    _mediumseagreen = new SolidColorBrush(_deviceContext, Color.MediumSeaGreen);
                }
                return _mediumseagreen;
            }
        }

        public static SolidColorBrush MediumSlateBlue
        {
            get
            {
                if (_mediumslateblue == null)
                {
                    _mediumslateblue = new SolidColorBrush(_deviceContext, Color.MediumSlateBlue);
                }
                return _mediumslateblue;
            }
        }

        public static SolidColorBrush MediumSpringGreen
        {
            get
            {
                if (_mediumspringgreen == null)
                {
                    _mediumspringgreen = new SolidColorBrush(_deviceContext, Color.MediumSpringGreen);
                }
                return _mediumspringgreen;
            }
        }

        public static SolidColorBrush MediumTurquoise
        {
            get
            {
                if (_mediumturquoise == null)
                {
                    _mediumturquoise = new SolidColorBrush(_deviceContext, Color.MediumTurquoise);
                }
                return _mediumturquoise;
            }
        }

        public static SolidColorBrush MediumVioletRed
        {
            get
            {
                if (_mediumvioletred == null)
                {
                    _mediumvioletred = new SolidColorBrush(_deviceContext, Color.MediumVioletRed);
                }
                return _mediumvioletred;
            }
        }

        public static SolidColorBrush MidnightBlue
        {
            get
            {
                if (_midnightblue == null)
                {
                    _midnightblue = new SolidColorBrush(_deviceContext, Color.MidnightBlue);
                }
                return _midnightblue;
            }
        }

        public static SolidColorBrush MintCream
        {
            get
            {
                if (_mintcream == null)
                {
                    _mintcream = new SolidColorBrush(_deviceContext, Color.MintCream);
                }
                return _mintcream;
            }
        }

        public static SolidColorBrush MistyRose
        {
            get
            {
                if (_mistyrose == null)
                {
                    _mistyrose = new SolidColorBrush(_deviceContext, Color.MistyRose);
                }
                return _mistyrose;
            }
        }

        public static SolidColorBrush Moccasin
        {
            get
            {
                if (_moccasin == null)
                {
                    _moccasin = new SolidColorBrush(_deviceContext, Color.Moccasin);
                }
                return _moccasin;
            }
        }

        public static SolidColorBrush NavajoWhite
        {
            get
            {
                if (_navajowhite == null)
                {
                    _navajowhite = new SolidColorBrush(_deviceContext, Color.NavajoWhite);
                }
                return _navajowhite;
            }
        }

        public static SolidColorBrush Navy
        {
            get
            {
                if (_navy == null)
                {
                    _navy = new SolidColorBrush(_deviceContext, Color.Navy);
                }
                return _navy;
            }
        }

        public static SolidColorBrush OldLace
        {
            get
            {
                if (_oldlace == null)
                {
                    _oldlace = new SolidColorBrush(_deviceContext, Color.OldLace);
                }
                return _oldlace;
            }
        }

        public static SolidColorBrush Olive
        {
            get
            {
                if (_olive == null)
                {
                    _olive = new SolidColorBrush(_deviceContext, Color.Olive);
                }
                return _olive;
            }
        }

        public static SolidColorBrush OliveDrab
        {
            get
            {
                if (_olivedrab == null)
                {
                    _olivedrab = new SolidColorBrush(_deviceContext, Color.OliveDrab);
                }
                return _olivedrab;
            }
        }

        public static SolidColorBrush Orange
        {
            get
            {
                if (_orange == null)
                {
                    _orange = new SolidColorBrush(_deviceContext, Color.Orange);
                }
                return _orange;
            }
        }

        public static SolidColorBrush OrangeRed
        {
            get
            {
                if (_orangered == null)
                {
                    _orangered = new SolidColorBrush(_deviceContext, Color.OrangeRed);
                }
                return _orangered;
            }
        }

        public static SolidColorBrush Orchid
        {
            get
            {
                if (_orchid == null)
                {
                    _orchid = new SolidColorBrush(_deviceContext, Color.Orchid);
                }
                return _orchid;
            }
        }

        public static SolidColorBrush PaleGoldenrod
        {
            get
            {
                if (_palegoldenrod == null)
                {
                    _palegoldenrod = new SolidColorBrush(_deviceContext, Color.PaleGoldenrod);
                }
                return _palegoldenrod;
            }
        }

        public static SolidColorBrush PaleGreen
        {
            get
            {
                if (_palegreen == null)
                {
                    _palegreen = new SolidColorBrush(_deviceContext, Color.PaleGreen);
                }
                return _palegreen;
            }
        }

        public static SolidColorBrush PaleTurquoise
        {
            get
            {
                if (_paleturquoise == null)
                {
                    _paleturquoise = new SolidColorBrush(_deviceContext, Color.PaleTurquoise);
                }
                return _paleturquoise;
            }
        }

        public static SolidColorBrush PaleVioletRed
        {
            get
            {
                if (_palevioletred == null)
                {
                    _palevioletred = new SolidColorBrush(_deviceContext, Color.PaleVioletRed);
                }
                return _palevioletred;
            }
        }

        public static SolidColorBrush PapayaWhip
        {
            get
            {
                if (_papayawhip == null)
                {
                    _papayawhip = new SolidColorBrush(_deviceContext, Color.PapayaWhip);
                }
                return _papayawhip;
            }
        }

        public static SolidColorBrush PeachPuff
        {
            get
            {
                if (_peachpuff == null)
                {
                    _peachpuff = new SolidColorBrush(_deviceContext, Color.PeachPuff);
                }
                return _peachpuff;
            }
        }

        public static SolidColorBrush Peru
        {
            get
            {
                if (_peru == null)
                {
                    _peru = new SolidColorBrush(_deviceContext, Color.Peru);
                }
                return _peru;
            }
        }

        public static SolidColorBrush Pink
        {
            get
            {
                if (_pink == null)
                {
                    _pink = new SolidColorBrush(_deviceContext, Color.Pink);
                }
                return _pink;
            }
        }

        public static SolidColorBrush Plum
        {
            get
            {
                if (_plum == null)
                {
                    _plum = new SolidColorBrush(_deviceContext, Color.Plum);
                }
                return _plum;
            }
        }

        public static SolidColorBrush PowderBlue
        {
            get
            {
                if (_powderblue == null)
                {
                    _powderblue = new SolidColorBrush(_deviceContext, Color.PowderBlue);
                }
                return _powderblue;
            }
        }

        public static SolidColorBrush Purple
        {
            get
            {
                if (_purple == null)
                {
                    _purple = new SolidColorBrush(_deviceContext, Color.Purple);
                }
                return _purple;
            }
        }

        public static SolidColorBrush Red
        {
            get
            {
                if (_red == null)
                {
                    _red = new SolidColorBrush(_deviceContext, Color.Red);
                }
                return _red;
            }
        }

        public static SolidColorBrush RosyBrown
        {
            get
            {
                if (_rosybrown == null)
                {
                    _rosybrown = new SolidColorBrush(_deviceContext, Color.RosyBrown);
                }
                return _rosybrown;
            }
        }

        public static SolidColorBrush RoyalBlue
        {
            get
            {
                if (_royalblue == null)
                {
                    _royalblue = new SolidColorBrush(_deviceContext, Color.RoyalBlue);
                }
                return _royalblue;
            }
        }

        public static SolidColorBrush SaddleBrown
        {
            get
            {
                if (_saddlebrown == null)
                {
                    _saddlebrown = new SolidColorBrush(_deviceContext, Color.SaddleBrown);
                }
                return _saddlebrown;
            }
        }

        public static SolidColorBrush Salmon
        {
            get
            {
                if (_salmon == null)
                {
                    _salmon = new SolidColorBrush(_deviceContext, Color.Salmon);
                }
                return _salmon;
            }
        }

        public static SolidColorBrush SandyBrown
        {
            get
            {
                if (_sandybrown == null)
                {
                    _sandybrown = new SolidColorBrush(_deviceContext, Color.SandyBrown);
                }
                return _sandybrown;
            }
        }

        public static SolidColorBrush SeaGreen
        {
            get
            {
                if (_seagreen == null)
                {
                    _seagreen = new SolidColorBrush(_deviceContext, Color.SeaGreen);
                }
                return _seagreen;
            }
        }

        public static SolidColorBrush SeaShell
        {
            get
            {
                if (_seashell == null)
                {
                    _seashell = new SolidColorBrush(_deviceContext, Color.SeaShell);
                }
                return _seashell;
            }
        }

        public static SolidColorBrush Sienna
        {
            get
            {
                if (_sienna == null)
                {
                    _sienna = new SolidColorBrush(_deviceContext, Color.Sienna);
                }
                return _sienna;
            }
        }

        public static SolidColorBrush Silver
        {
            get
            {
                if (_silver == null)
                {
                    _silver = new SolidColorBrush(_deviceContext, Color.Silver);
                }
                return _silver;
            }
        }

        public static SolidColorBrush SkyBlue
        {
            get
            {
                if (_skyblue == null)
                {
                    _skyblue = new SolidColorBrush(_deviceContext, Color.SkyBlue);
                }
                return _skyblue;
            }
        }

        public static SolidColorBrush SlateBlue
        {
            get
            {
                if (_slateblue == null)
                {
                    _slateblue = new SolidColorBrush(_deviceContext, Color.SlateBlue);
                }
                return _slateblue;
            }
        }

        public static SolidColorBrush SlateGray
        {
            get
            {
                if (_slategray == null)
                {
                    _slategray = new SolidColorBrush(_deviceContext, Color.SlateGray);
                }
                return _slategray;
            }
        }

        public static SolidColorBrush Snow
        {
            get
            {
                if (_snow == null)
                {
                    _snow = new SolidColorBrush(_deviceContext, Color.Snow);
                }
                return _snow;
            }
        }

        public static SolidColorBrush SpringGreen
        {
            get
            {
                if (_springgreen == null)
                {
                    _springgreen = new SolidColorBrush(_deviceContext, Color.SpringGreen);
                }
                return _springgreen;
            }
        }

        public static SolidColorBrush SteelBlue
        {
            get
            {
                if (_steelblue == null)
                {
                    _steelblue = new SolidColorBrush(_deviceContext, Color.SteelBlue);
                }
                return _steelblue;
            }
        }

        public static SolidColorBrush Tan
        {
            get
            {
                if (_tan == null)
                {
                    _tan = new SolidColorBrush(_deviceContext, Color.Tan);
                }
                return _tan;
            }
        }

        public static SolidColorBrush Teal
        {
            get
            {
                if (_teal == null)
                {
                    _teal = new SolidColorBrush(_deviceContext, Color.Teal);
                }
                return _teal;
            }
        }

        public static SolidColorBrush Thistle
        {
            get
            {
                if (_thistle == null)
                {
                    _thistle = new SolidColorBrush(_deviceContext, Color.Thistle);
                }
                return _thistle;
            }
        }

        public static SolidColorBrush Tomato
        {
            get
            {
                if (_tomato == null)
                {
                    _tomato = new SolidColorBrush(_deviceContext, Color.Tomato);
                }
                return _tomato;
            }
        }

        public static SolidColorBrush Turquoise
        {
            get
            {
                if (_turquoise == null)
                {
                    _turquoise = new SolidColorBrush(_deviceContext, Color.Turquoise);
                }
                return _turquoise;
            }
        }

        public static SolidColorBrush Violet
        {
            get
            {
                if (_violet == null)
                {
                    _violet = new SolidColorBrush(_deviceContext, Color.Violet);
                }
                return _violet;
            }
        }

        public static SolidColorBrush Wheat
        {
            get
            {
                if (_wheat == null)
                {
                    _wheat = new SolidColorBrush(_deviceContext, Color.Wheat);
                }
                return _wheat;
            }
        }

        public static SolidColorBrush White
        {
            get
            {
                if (_white == null)
                {
                    _white = new SolidColorBrush(_deviceContext, Color.White);
                }
                return _white;
            }
        }

        public static SolidColorBrush WhiteSmoke
        {
            get
            {
                if (_whitesmoke == null)
                {
                    _whitesmoke = new SolidColorBrush(_deviceContext, Color.WhiteSmoke);
                }
                return _whitesmoke;
            }
        }

        public static SolidColorBrush Yellow
        {
            get
            {
                if (_yellow == null)
                {
                    _yellow = new SolidColorBrush(_deviceContext, Color.Yellow);
                }
                return _yellow;
            }
        }

        public static SolidColorBrush YellowGreen
        {
            get
            {
                if (_yellowgreen == null)
                {
                    _yellowgreen = new SolidColorBrush(_deviceContext, Color.YellowGreen);
                }
                return _yellowgreen;
            }
        }

        #endregion

        #region Field initialization

        //public static SolidColorBrush Zero {
        //    get { return _zero; }
        //}

        //public static SolidColorBrush Transparent {
        //    get { return _transparent; }
        //}

        //public static SolidColorBrush AliceBlue {
        //    get { return _aliceblue; }
        //}

        //public static SolidColorBrush AntiqueWhite {
        //    get { return _antiquewhite; }
        //}

        //public static SolidColorBrush Aqua {
        //    get { return _aqua; }
        //}

        //public static SolidColorBrush Aquamarine {
        //    get { return _aquamarine; }
        //}

        //public static SolidColorBrush Azure {
        //    get { return _azure; }
        //}

        //public static SolidColorBrush Beige {
        //    get { return _beige; }
        //}

        //public static SolidColorBrush Bisque {
        //    get { return _bisque; }
        //}

        //public static SolidColorBrush Black {
        //    get { return _black; }
        //}

        //public static SolidColorBrush BlanchedAlmond {
        //    get { return _blanchedalmond; }
        //}

        //public static SolidColorBrush Blue {
        //    get { return _blue; }
        //}

        //public static SolidColorBrush BlueViolet {
        //    get { return _blueviolet; }
        //}

        //public static SolidColorBrush Brown {
        //    get { return _brown; }
        //}

        //public static SolidColorBrush BurlyWood {
        //    get { return _burlywood; }
        //}

        //public static SolidColorBrush CadetBlue {
        //    get { return _cadetblue; }
        //}

        //public static SolidColorBrush Chartreuse {
        //    get { return _chartreuse; }
        //}

        //public static SolidColorBrush Chocolate {
        //    get { return _chocolate; }
        //}

        //public static SolidColorBrush Coral {
        //    get { return _coral; }
        //}

        //public static SolidColorBrush CornflowerBlue {
        //    get { return _cornflowerblue; }
        //}

        //public static SolidColorBrush Cornsilk {
        //    get { return _cornsilk; }
        //}

        //public static SolidColorBrush Crimson {
        //    get { return _crimson; }
        //}

        //public static SolidColorBrush Cyan {
        //    get { return _cyan; }
        //}

        //public static SolidColorBrush DarkBlue {
        //    get { return _darkblue; }
        //}

        //public static SolidColorBrush DarkCyan {
        //    get { return _darkcyan; }
        //}

        //public static SolidColorBrush DarkGoldenrod {
        //    get { return _darkgoldenrod; }
        //}

        //public static SolidColorBrush DarkGray {
        //    get { return _darkgray; }
        //}

        //public static SolidColorBrush DarkGreen {
        //    get { return _darkgreen; }
        //}

        //public static SolidColorBrush DarkKhaki {
        //    get { return _darkkhaki; }
        //}

        //public static SolidColorBrush DarkMagenta {
        //    get { return _darkmagenta; }
        //}

        //public static SolidColorBrush DarkOliveGreen {
        //    get { return _darkolivegreen; }
        //}

        //public static SolidColorBrush DarkOrange {
        //    get { return _darkorange; }
        //}

        //public static SolidColorBrush DarkOrchid {
        //    get { return _darkorchid; }
        //}

        //public static SolidColorBrush DarkRed {
        //    get { return _darkred; }
        //}

        //public static SolidColorBrush DarkSalmon {
        //    get { return _darksalmon; }
        //}

        //public static SolidColorBrush DarkSeaGreen {
        //    get { return _darkseagreen; }
        //}

        //public static SolidColorBrush DarkSlateBlue {
        //    get { return _darkslateblue; }
        //}

        //public static SolidColorBrush DarkSlateGray {
        //    get { return _darkslategray; }
        //}

        //public static SolidColorBrush DarkTurquoise {
        //    get { return _darkturquoise; }
        //}

        //public static SolidColorBrush DarkViolet {
        //    get { return _darkviolet; }
        //}

        //public static SolidColorBrush DeepPink {
        //    get { return _deeppink; }
        //}

        //public static SolidColorBrush DeepSkyBlue {
        //    get { return _deepskyblue; }
        //}

        //public static SolidColorBrush DimGray {
        //    get { return _dimgray; }
        //}

        //public static SolidColorBrush DodgerBlue {
        //    get { return _dodgerblue; }
        //}

        //public static SolidColorBrush Firebrick {
        //    get { return _firebrick; }
        //}

        //public static SolidColorBrush FloralWhite {
        //    get { return _floralwhite; }
        //}

        //public static SolidColorBrush ForestGreen {
        //    get { return _forestgreen; }
        //}

        //public static SolidColorBrush Fuchsia {
        //    get { return _fuchsia; }
        //}

        //public static SolidColorBrush Gainsboro {
        //    get { return _gainsboro; }
        //}

        //public static SolidColorBrush GhostWhite {
        //    get { return _ghostwhite; }
        //}

        //public static SolidColorBrush Gold {
        //    get { return _gold; }
        //}

        //public static SolidColorBrush Goldenrod {
        //    get { return _goldenrod; }
        //}

        //public static SolidColorBrush Gray {
        //    get { return _gray; }
        //}

        //public static SolidColorBrush Green {
        //    get { return _green; }
        //}

        //public static SolidColorBrush GreenYellow {
        //    get { return _greenyellow; }
        //}

        //public static SolidColorBrush Honeydew {
        //    get { return _honeydew; }
        //}

        //public static SolidColorBrush HotPink {
        //    get { return _hotpink; }
        //}

        //public static SolidColorBrush IndianRed {
        //    get { return _indianred; }
        //}

        //public static SolidColorBrush Indigo {
        //    get { return _indigo; }
        //}

        //public static SolidColorBrush Ivory {
        //    get { return _ivory; }
        //}

        //public static SolidColorBrush Khaki {
        //    get { return _khaki; }
        //}

        //public static SolidColorBrush Lavender {
        //    get { return _lavender; }
        //}

        //public static SolidColorBrush LavenderBlush {
        //    get { return _lavenderblush; }
        //}

        //public static SolidColorBrush LawnGreen {
        //    get { return _lawngreen; }
        //}

        //public static SolidColorBrush LemonChiffon {
        //    get { return _lemonchiffon; }
        //}

        //public static SolidColorBrush LightBlue {
        //    get { return _lightblue; }
        //}

        //public static SolidColorBrush LightCoral {
        //    get { return _lightcoral; }
        //}

        //public static SolidColorBrush LightCyan {
        //    get { return _lightcyan; }
        //}

        //public static SolidColorBrush LightGoldenrodYellow {
        //    get { return _lightgoldenrodyellow; }
        //}

        //public static SolidColorBrush LightGray {
        //    get { return _lightgray; }
        //}

        //public static SolidColorBrush LightGreen {
        //    get { return _lightgreen; }
        //}

        //public static SolidColorBrush LightPink {
        //    get { return _lightpink; }
        //}

        //public static SolidColorBrush LightSalmon {
        //    get { return _lightsalmon; }
        //}

        //public static SolidColorBrush LightSeaGreen {
        //    get { return _lightseagreen; }
        //}

        //public static SolidColorBrush LightSkyBlue {
        //    get { return _lightskyblue; }
        //}

        //public static SolidColorBrush LightSlateGray {
        //    get { return _lightslategray; }
        //}

        //public static SolidColorBrush LightSteelBlue {
        //    get { return _lightsteelblue; }
        //}

        //public static SolidColorBrush LightYellow {
        //    get { return _lightyellow; }
        //}

        //public static SolidColorBrush Lime {
        //    get { return _lime; }
        //}

        //public static SolidColorBrush LimeGreen {
        //    get { return _limegreen; }
        //}

        //public static SolidColorBrush Linen {
        //    get { return _linen; }
        //}

        //public static SolidColorBrush Magenta {
        //    get { return _magenta; }
        //}

        //public static SolidColorBrush Maroon {
        //    get { return _maroon; }
        //}

        //public static SolidColorBrush MediumAquamarine {
        //    get { return _mediumaquamarine; }
        //}

        //public static SolidColorBrush MediumBlue {
        //    get { return _mediumblue; }
        //}

        //public static SolidColorBrush MediumOrchid {
        //    get { return _mediumorchid; }
        //}

        //public static SolidColorBrush MediumPurple {
        //    get { return _mediumpurple; }
        //}

        //public static SolidColorBrush MediumSeaGreen {
        //    get { return _mediumseagreen; }
        //}

        //public static SolidColorBrush MediumSlateBlue {
        //    get { return _mediumslateblue; }
        //}

        //public static SolidColorBrush MediumSpringGreen {
        //    get { return _mediumspringgreen; }
        //}

        //public static SolidColorBrush MediumTurquoise {
        //    get { return _mediumturquoise; }
        //}

        //public static SolidColorBrush MediumVioletRed {
        //    get { return _mediumvioletred; }
        //}

        //public static SolidColorBrush MidnightBlue {
        //    get { return _midnightblue; }
        //}

        //public static SolidColorBrush MintCream {
        //    get { return _mintcream; }
        //}

        //public static SolidColorBrush MistyRose {
        //    get { return _mistyrose; }
        //}

        //public static SolidColorBrush Moccasin {
        //    get { return _moccasin; }
        //}

        //public static SolidColorBrush NavajoWhite {
        //    get { return _navajowhite; }
        //}

        //public static SolidColorBrush Navy {
        //    get { return _navy; }
        //}

        //public static SolidColorBrush OldLace {
        //    get { return _oldlace; }
        //}

        //public static SolidColorBrush Olive {
        //    get { return _olive; }
        //}

        //public static SolidColorBrush OliveDrab {
        //    get { return _olivedrab; }
        //}

        //public static SolidColorBrush Orange {
        //    get { return _orange; }
        //}

        //public static SolidColorBrush OrangeRed {
        //    get { return _orangered; }
        //}

        //public static SolidColorBrush Orchid {
        //    get { return _orchid; }
        //}

        //public static SolidColorBrush PaleGoldenrod {
        //    get { return _palegoldenrod; }
        //}

        //public static SolidColorBrush PaleGreen {
        //    get { return _palegreen; }
        //}

        //public static SolidColorBrush PaleTurquoise {
        //    get { return _paleturquoise; }
        //}

        //public static SolidColorBrush PaleVioletRed {
        //    get { return _palevioletred; }
        //}

        //public static SolidColorBrush PapayaWhip {
        //    get { return _papayawhip; }
        //}

        //public static SolidColorBrush PeachPuff {
        //    get { return _peachpuff; }
        //}

        //public static SolidColorBrush Peru {
        //    get { return _peru; }
        //}

        //public static SolidColorBrush Pink {
        //    get { return _pink; }
        //}

        //public static SolidColorBrush Plum {
        //    get { return _plum; }
        //}

        //public static SolidColorBrush PowderBlue {
        //    get { return _powderblue; }
        //}

        //public static SolidColorBrush Purple {
        //    get { return _purple; }
        //}

        //public static SolidColorBrush Red {
        //    get { return _red; }
        //}

        //public static SolidColorBrush RosyBrown {
        //    get { return _rosybrown; }
        //}

        //public static SolidColorBrush RoyalBlue {
        //    get { return _royalblue; }
        //}

        //public static SolidColorBrush SaddleBrown {
        //    get { return _saddlebrown; }
        //}

        //public static SolidColorBrush Salmon {
        //    get { return _salmon; }
        //}

        //public static SolidColorBrush SandyBrown {
        //    get { return _sandybrown; }
        //}

        //public static SolidColorBrush SeaGreen {
        //    get { return _seagreen; }
        //}

        //public static SolidColorBrush SeaShell {
        //    get { return _seashell; }
        //}

        //public static SolidColorBrush Sienna {
        //    get { return _sienna; }
        //}

        //public static SolidColorBrush Silver {
        //    get { return _silver; }
        //}

        //public static SolidColorBrush SkyBlue {
        //    get { return _skyblue; }
        //}

        //public static SolidColorBrush SlateBlue {
        //    get { return _slateblue; }
        //}

        //public static SolidColorBrush SlateGray {
        //    get { return _slategray; }
        //}

        //public static SolidColorBrush Snow {
        //    get { return _snow; }
        //}

        //public static SolidColorBrush SpringGreen {
        //    get { return _springgreen; }
        //}

        //public static SolidColorBrush SteelBlue {
        //    get { return _steelblue; }
        //}

        //public static SolidColorBrush Tan {
        //    get { return _tan; }
        //}

        //public static SolidColorBrush Teal {
        //    get { return _teal; }
        //}

        //public static SolidColorBrush Thistle {
        //    get { return _thistle; }
        //}

        //public static SolidColorBrush Tomato {
        //    get { return _tomato; }
        //}

        //public static SolidColorBrush Turquoise {
        //    get { return _turquoise; }
        //}

        //public static SolidColorBrush Violet {
        //    get { return _violet; }
        //}

        //public static SolidColorBrush Wheat {
        //    get { return _wheat; }
        //}

        //public static SolidColorBrush White {
        //    get { return _white; }
        //}

        //public static SolidColorBrush WhiteSmoke {
        //    get { return _whitesmoke; }
        //}

        //public static SolidColorBrush Yellow {
        //    get { return _yellow; }
        //}

        //public static SolidColorBrush YellowGreen {
        //    get { return _yellowgreen; }
        //}


        //private static readonly SolidColorBrush _zero = new SolidColorBrush(_deviceContext, Color.Zero);
        //private static readonly SolidColorBrush _transparent = new SolidColorBrush(_deviceContext, Color.Transparent);
        //private static readonly SolidColorBrush _aliceblue = new SolidColorBrush(_deviceContext, Color.AliceBlue);
        //private static readonly SolidColorBrush _antiquewhite = new SolidColorBrush(_deviceContext, Color.AntiqueWhite);
        //private static readonly SolidColorBrush _aqua = new SolidColorBrush(_deviceContext, Color.Aqua);
        //private static readonly SolidColorBrush _aquamarine = new SolidColorBrush(_deviceContext, Color.Aquamarine);
        //private static readonly SolidColorBrush _azure = new SolidColorBrush(_deviceContext, Color.Azure);
        //private static readonly SolidColorBrush _beige = new SolidColorBrush(_deviceContext, Color.Beige);
        //private static readonly SolidColorBrush _bisque = new SolidColorBrush(_deviceContext, Color.Bisque);
        //private static readonly SolidColorBrush _black = new SolidColorBrush(_deviceContext, Color.Black);

        //private static readonly SolidColorBrush _blanchedalmond = new SolidColorBrush(_deviceContext,
        //    Color.BlanchedAlmond);

        //private static readonly SolidColorBrush _blue = new SolidColorBrush(_deviceContext, Color.Blue);
        //private static readonly SolidColorBrush _blueviolet = new SolidColorBrush(_deviceContext, Color.BlueViolet);
        //private static readonly SolidColorBrush _brown = new SolidColorBrush(_deviceContext, Color.Brown);
        //private static readonly SolidColorBrush _burlywood = new SolidColorBrush(_deviceContext, Color.BurlyWood);
        //private static readonly SolidColorBrush _cadetblue = new SolidColorBrush(_deviceContext, Color.CadetBlue);
        //private static readonly SolidColorBrush _chartreuse = new SolidColorBrush(_deviceContext, Color.Chartreuse);
        //private static readonly SolidColorBrush _chocolate = new SolidColorBrush(_deviceContext, Color.Chocolate);
        //private static readonly SolidColorBrush _coral = new SolidColorBrush(_deviceContext, Color.Coral);

        //private static readonly SolidColorBrush _cornflowerblue = new SolidColorBrush(_deviceContext,
        //    Color.CornflowerBlue);

        //private static readonly SolidColorBrush _cornsilk = new SolidColorBrush(_deviceContext, Color.Cornsilk);
        //private static readonly SolidColorBrush _crimson = new SolidColorBrush(_deviceContext, Color.Crimson);
        //private static readonly SolidColorBrush _cyan = new SolidColorBrush(_deviceContext, Color.Cyan);
        //private static readonly SolidColorBrush _darkblue = new SolidColorBrush(_deviceContext, Color.DarkBlue);
        //private static readonly SolidColorBrush _darkcyan = new SolidColorBrush(_deviceContext, Color.DarkCyan);
        //private static readonly SolidColorBrush _darkgoldenrod = new SolidColorBrush(_deviceContext, Color.DarkGoldenrod);
        //private static readonly SolidColorBrush _darkgray = new SolidColorBrush(_deviceContext, Color.DarkGray);
        //private static readonly SolidColorBrush _darkgreen = new SolidColorBrush(_deviceContext, Color.DarkGreen);
        //private static readonly SolidColorBrush _darkkhaki = new SolidColorBrush(_deviceContext, Color.DarkKhaki);
        //private static readonly SolidColorBrush _darkmagenta = new SolidColorBrush(_deviceContext, Color.DarkMagenta);

        //private static readonly SolidColorBrush _darkolivegreen = new SolidColorBrush(_deviceContext,
        //    Color.DarkOliveGreen);

        //private static readonly SolidColorBrush _darkorange = new SolidColorBrush(_deviceContext, Color.DarkOrange);
        //private static readonly SolidColorBrush _darkorchid = new SolidColorBrush(_deviceContext, Color.DarkOrchid);
        //private static readonly SolidColorBrush _darkred = new SolidColorBrush(_deviceContext, Color.DarkRed);
        //private static readonly SolidColorBrush _darksalmon = new SolidColorBrush(_deviceContext, Color.DarkSalmon);
        //private static readonly SolidColorBrush _darkseagreen = new SolidColorBrush(_deviceContext, Color.DarkSeaGreen);
        //private static readonly SolidColorBrush _darkslateblue = new SolidColorBrush(_deviceContext, Color.DarkSlateBlue);
        //private static readonly SolidColorBrush _darkslategray = new SolidColorBrush(_deviceContext, Color.DarkSlateGray);
        //private static readonly SolidColorBrush _darkturquoise = new SolidColorBrush(_deviceContext, Color.DarkTurquoise);
        //private static readonly SolidColorBrush _darkviolet = new SolidColorBrush(_deviceContext, Color.DarkViolet);
        //private static readonly SolidColorBrush _deeppink = new SolidColorBrush(_deviceContext, Color.DeepPink);
        //private static readonly SolidColorBrush _deepskyblue = new SolidColorBrush(_deviceContext, Color.DeepSkyBlue);
        //private static readonly SolidColorBrush _dimgray = new SolidColorBrush(_deviceContext, Color.DimGray);
        //private static readonly SolidColorBrush _dodgerblue = new SolidColorBrush(_deviceContext, Color.DodgerBlue);
        //private static readonly SolidColorBrush _firebrick = new SolidColorBrush(_deviceContext, Color.Firebrick);
        //private static readonly SolidColorBrush _floralwhite = new SolidColorBrush(_deviceContext, Color.FloralWhite);
        //private static readonly SolidColorBrush _forestgreen = new SolidColorBrush(_deviceContext, Color.ForestGreen);
        //private static readonly SolidColorBrush _fuchsia = new SolidColorBrush(_deviceContext, Color.Fuchsia);
        //private static readonly SolidColorBrush _gainsboro = new SolidColorBrush(_deviceContext, Color.Gainsboro);
        //private static readonly SolidColorBrush _ghostwhite = new SolidColorBrush(_deviceContext, Color.GhostWhite);
        //private static readonly SolidColorBrush _gold = new SolidColorBrush(_deviceContext, Color.Gold);
        //private static readonly SolidColorBrush _goldenrod = new SolidColorBrush(_deviceContext, Color.Goldenrod);
        //private static readonly SolidColorBrush _gray = new SolidColorBrush(_deviceContext, Color.Gray);
        //private static readonly SolidColorBrush _green = new SolidColorBrush(_deviceContext, Color.Green);
        //private static readonly SolidColorBrush _greenyellow = new SolidColorBrush(_deviceContext, Color.GreenYellow);
        //private static readonly SolidColorBrush _honeydew = new SolidColorBrush(_deviceContext, Color.Honeydew);
        //private static readonly SolidColorBrush _hotpink = new SolidColorBrush(_deviceContext, Color.HotPink);
        //private static readonly SolidColorBrush _indianred = new SolidColorBrush(_deviceContext, Color.IndianRed);
        //private static readonly SolidColorBrush _indigo = new SolidColorBrush(_deviceContext, Color.Indigo);
        //private static readonly SolidColorBrush _ivory = new SolidColorBrush(_deviceContext, Color.Ivory);
        //private static readonly SolidColorBrush _khaki = new SolidColorBrush(_deviceContext, Color.Khaki);
        //private static readonly SolidColorBrush _lavender = new SolidColorBrush(_deviceContext, Color.Lavender);
        //private static readonly SolidColorBrush _lavenderblush = new SolidColorBrush(_deviceContext, Color.LavenderBlush);
        //private static readonly SolidColorBrush _lawngreen = new SolidColorBrush(_deviceContext, Color.LawnGreen);
        //private static readonly SolidColorBrush _lemonchiffon = new SolidColorBrush(_deviceContext, Color.LemonChiffon);
        //private static readonly SolidColorBrush _lightblue = new SolidColorBrush(_deviceContext, Color.LightBlue);
        //private static readonly SolidColorBrush _lightcoral = new SolidColorBrush(_deviceContext, Color.LightCoral);
        //private static readonly SolidColorBrush _lightcyan = new SolidColorBrush(_deviceContext, Color.LightCyan);

        //private static readonly SolidColorBrush _lightgoldenrodyellow = new SolidColorBrush(_deviceContext,
        //    Color.LightGoldenrodYellow);

        //private static readonly SolidColorBrush _lightgray = new SolidColorBrush(_deviceContext, Color.LightGray);
        //private static readonly SolidColorBrush _lightgreen = new SolidColorBrush(_deviceContext, Color.LightGreen);
        //private static readonly SolidColorBrush _lightpink = new SolidColorBrush(_deviceContext, Color.LightPink);
        //private static readonly SolidColorBrush _lightsalmon = new SolidColorBrush(_deviceContext, Color.LightSalmon);
        //private static readonly SolidColorBrush _lightseagreen = new SolidColorBrush(_deviceContext, Color.LightSeaGreen);
        //private static readonly SolidColorBrush _lightskyblue = new SolidColorBrush(_deviceContext, Color.LightSkyBlue);

        //private static readonly SolidColorBrush _lightslategray = new SolidColorBrush(_deviceContext,
        //    Color.LightSlateGray);

        //private static readonly SolidColorBrush _lightsteelblue = new SolidColorBrush(_deviceContext,
        //    Color.LightSteelBlue);

        //private static readonly SolidColorBrush _lightyellow = new SolidColorBrush(_deviceContext, Color.LightYellow);
        //private static readonly SolidColorBrush _lime = new SolidColorBrush(_deviceContext, Color.Lime);
        //private static readonly SolidColorBrush _limegreen = new SolidColorBrush(_deviceContext, Color.LimeGreen);
        //private static readonly SolidColorBrush _linen = new SolidColorBrush(_deviceContext, Color.Linen);
        //private static readonly SolidColorBrush _magenta = new SolidColorBrush(_deviceContext, Color.Magenta);
        //private static readonly SolidColorBrush _maroon = new SolidColorBrush(_deviceContext, Color.Maroon);

        //private static readonly SolidColorBrush _mediumaquamarine = new SolidColorBrush(_deviceContext,
        //    Color.MediumAquamarine);

        //private static readonly SolidColorBrush _mediumblue = new SolidColorBrush(_deviceContext, Color.MediumBlue);
        //private static readonly SolidColorBrush _mediumorchid = new SolidColorBrush(_deviceContext, Color.MediumOrchid);
        //private static readonly SolidColorBrush _mediumpurple = new SolidColorBrush(_deviceContext, Color.MediumPurple);

        //private static readonly SolidColorBrush _mediumseagreen = new SolidColorBrush(_deviceContext,
        //    Color.MediumSeaGreen);

        //private static readonly SolidColorBrush _mediumslateblue = new SolidColorBrush(_deviceContext,
        //    Color.MediumSlateBlue);

        //private static readonly SolidColorBrush _mediumspringgreen = new SolidColorBrush(_deviceContext,
        //    Color.MediumSpringGreen);

        //private static readonly SolidColorBrush _mediumturquoise = new SolidColorBrush(_deviceContext,
        //    Color.MediumTurquoise);

        //private static readonly SolidColorBrush _mediumvioletred = new SolidColorBrush(_deviceContext,
        //    Color.MediumVioletRed);

        //private static readonly SolidColorBrush _midnightblue = new SolidColorBrush(_deviceContext, Color.MidnightBlue);
        //private static readonly SolidColorBrush _mintcream = new SolidColorBrush(_deviceContext, Color.MintCream);
        //private static readonly SolidColorBrush _mistyrose = new SolidColorBrush(_deviceContext, Color.MistyRose);
        //private static readonly SolidColorBrush _moccasin = new SolidColorBrush(_deviceContext, Color.Moccasin);
        //private static readonly SolidColorBrush _navajowhite = new SolidColorBrush(_deviceContext, Color.NavajoWhite);
        //private static readonly SolidColorBrush _navy = new SolidColorBrush(_deviceContext, Color.Navy);
        //private static readonly SolidColorBrush _oldlace = new SolidColorBrush(_deviceContext, Color.OldLace);
        //private static readonly SolidColorBrush _olive = new SolidColorBrush(_deviceContext, Color.Olive);
        //private static readonly SolidColorBrush _olivedrab = new SolidColorBrush(_deviceContext, Color.OliveDrab);
        //private static readonly SolidColorBrush _orange = new SolidColorBrush(_deviceContext, Color.Orange);
        //private static readonly SolidColorBrush _orangered = new SolidColorBrush(_deviceContext, Color.OrangeRed);
        //private static readonly SolidColorBrush _orchid = new SolidColorBrush(_deviceContext, Color.Orchid);
        //private static readonly SolidColorBrush _palegoldenrod = new SolidColorBrush(_deviceContext, Color.PaleGoldenrod);
        //private static readonly SolidColorBrush _palegreen = new SolidColorBrush(_deviceContext, Color.PaleGreen);
        //private static readonly SolidColorBrush _paleturquoise = new SolidColorBrush(_deviceContext, Color.PaleTurquoise);
        //private static readonly SolidColorBrush _palevioletred = new SolidColorBrush(_deviceContext, Color.PaleVioletRed);
        //private static readonly SolidColorBrush _papayawhip = new SolidColorBrush(_deviceContext, Color.PapayaWhip);
        //private static readonly SolidColorBrush _peachpuff = new SolidColorBrush(_deviceContext, Color.PeachPuff);
        //private static readonly SolidColorBrush _peru = new SolidColorBrush(_deviceContext, Color.Peru);
        //private static readonly SolidColorBrush _pink = new SolidColorBrush(_deviceContext, Color.Pink);
        //private static readonly SolidColorBrush _plum = new SolidColorBrush(_deviceContext, Color.Plum);
        //private static readonly SolidColorBrush _powderblue = new SolidColorBrush(_deviceContext, Color.PowderBlue);
        //private static readonly SolidColorBrush _purple = new SolidColorBrush(_deviceContext, Color.Purple);
        //private static readonly SolidColorBrush _red = new SolidColorBrush(_deviceContext, Color.Red);
        //private static readonly SolidColorBrush _rosybrown = new SolidColorBrush(_deviceContext, Color.RosyBrown);
        //private static readonly SolidColorBrush _royalblue = new SolidColorBrush(_deviceContext, Color.RoyalBlue);
        //private static readonly SolidColorBrush _saddlebrown = new SolidColorBrush(_deviceContext, Color.SaddleBrown);
        //private static readonly SolidColorBrush _salmon = new SolidColorBrush(_deviceContext, Color.Salmon);
        //private static readonly SolidColorBrush _sandybrown = new SolidColorBrush(_deviceContext, Color.SandyBrown);
        //private static readonly SolidColorBrush _seagreen = new SolidColorBrush(_deviceContext, Color.SeaGreen);
        //private static readonly SolidColorBrush _seashell = new SolidColorBrush(_deviceContext, Color.SeaShell);
        //private static readonly SolidColorBrush _sienna = new SolidColorBrush(_deviceContext, Color.Sienna);
        //private static readonly SolidColorBrush _silver = new SolidColorBrush(_deviceContext, Color.Silver);
        //private static readonly SolidColorBrush _skyblue = new SolidColorBrush(_deviceContext, Color.SkyBlue);
        //private static readonly SolidColorBrush _slateblue = new SolidColorBrush(_deviceContext, Color.SlateBlue);
        //private static readonly SolidColorBrush _slategray = new SolidColorBrush(_deviceContext, Color.SlateGray);
        //private static readonly SolidColorBrush _snow = new SolidColorBrush(_deviceContext, Color.Snow);
        //private static readonly SolidColorBrush _springgreen = new SolidColorBrush(_deviceContext, Color.SpringGreen);
        //private static readonly SolidColorBrush _steelblue = new SolidColorBrush(_deviceContext, Color.SteelBlue);
        //private static readonly SolidColorBrush _tan = new SolidColorBrush(_deviceContext, Color.Tan);
        //private static readonly SolidColorBrush _teal = new SolidColorBrush(_deviceContext, Color.Teal);
        //private static readonly SolidColorBrush _thistle = new SolidColorBrush(_deviceContext, Color.Thistle);
        //private static readonly SolidColorBrush _tomato = new SolidColorBrush(_deviceContext, Color.Tomato);
        //private static readonly SolidColorBrush _turquoise = new SolidColorBrush(_deviceContext, Color.Turquoise);
        //private static readonly SolidColorBrush _violet = new SolidColorBrush(_deviceContext, Color.Violet);
        //private static readonly SolidColorBrush _wheat = new SolidColorBrush(_deviceContext, Color.Wheat);
        //private static readonly SolidColorBrush _white = new SolidColorBrush(_deviceContext, Color.White);
        //private static readonly SolidColorBrush _whitesmoke = new SolidColorBrush(_deviceContext, Color.WhiteSmoke);
        //private static readonly SolidColorBrush _yellow = new SolidColorBrush(_deviceContext, Color.Yellow);
        //private static readonly SolidColorBrush _yellowgreen = new SolidColorBrush(_deviceContext, Color.YellowGreen);

        #endregion
    }
}