using UnityEngine;

namespace MineSweeper
{
    public struct Cell
    {
        public enum Type
        {
            Invalid, Empty, Mine, Number
        }
        public Type type;
        /*  vector3 float deger donduruyor fakat grid yapilarda calisirken 
        vector3int kullanarak degeri integer olarak alabiliyoruz.   */
        public Vector3Int position;//prop yapilabilir mi?
        public int numberValue;
        public bool revealed;
        public bool flagged;
        public bool exploded;
    }
}