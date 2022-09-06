namespace ShapeLibrary
{
    public static class ShapeHandler
    {
        public static double GetShapeArea(IArea shape)
        {
            return shape.GetArea();
        }
    }
}
