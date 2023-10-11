namespace __ProjectCodeNeon.ImplantsRenderSystem.DataTypes
{
    public interface IRenderableImplant
    {
        int Id { get; }
        ImplantPlacement Placement { get; }
    }
}