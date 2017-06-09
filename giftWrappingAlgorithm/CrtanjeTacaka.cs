using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace giftWrappingAlgorithm
{
    public static class CrtanjeTacaka
    {
        private static LinkedList<Point> _tacke;
        private static LinkedList<Point> konveksniOmotac;

        public static LinkedList<Point> Calculate(LinkedList<Point> tacke)
        {
            if (tacke.Count < 3)
                return null;

            _tacke = new LinkedList<Point>(tacke);
            konveksniOmotac = new LinkedList<Point>();

            var s0 = NaciNajmanjuLijevuTacku();
            konveksniOmotac.AddLast(s0);
            _tacke.Remove(s0);
            _tacke.AddLast(s0);

            while (true)
            {
                var s1 = _tacke.First.Value;
                foreach (var p in _tacke)
                    if (Direction.Izracunati(s0, s1, p) == Direction.Value.Lijevo)
                        s1 = p;
                if (s1 == konveksniOmotac.First.Value)
                    break;

                konveksniOmotac.AddLast(s1);
                _tacke.Remove(s1);
                s0 = s1;
            }

            return konveksniOmotac;
        }

        private static Point NaciNajmanjuLijevuTacku()
        {
            var res = _tacke.First;
            var node = res.Next;

            while (node != null)
            {
                if (node.Value.X < res.Value.X)
                    res = node;

                if (node.Value.X == res.Value.X &&
                    node.Value.Y < res.Value.Y)
                    res = node;

                node = node.Next;
            }

            return res.Value;
        }
    }
}
