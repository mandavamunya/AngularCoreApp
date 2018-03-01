using System.Collections.Generic;

namespace Application.Extensions.Math.Generic
{
    public class Sets<T>
    {
        public static IEnumerable<T> Complement(List<T> A, List<T> B)
        {

            if (A.Count > B.Count)
                return NewList(A, B);
            else
                return NewList(B, A);
        }

        public static IEnumerable<T> NewList(List<T> big, List<T> small)
        {
            List<T> complement = new List<T>();
            foreach (T element in big)
                if (!small.Contains(element))
                    complement.Add(element);
            return complement;
        }

    }
}
