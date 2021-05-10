using UnityEngine;

public class Enumerators
{
    public enum AtomType
    {
        Hydrogen,
        Boron,
        Carbon,
        Nitrogen,
        Oxygen,
        Fluorine,
        Chlorine,
        Bromine,
        Iodine,
    }

    public enum AtomSymbol
    {
        H,
        B,
        C,
        N,
        O,
        F,
        Cl,
        Br,
        I,
    }

    public enum BondType
    {
        SingleBond,
        DoubleBond,
        TripleBond,
        AromaticBond,
    }

    public enum EGeometry
    {
        Terminal,
        Linear,
        TrigonalPlanar,
        Tetrahedral,
        TrigonalBipyramidal,
        Octahedral,
    }

    public enum OrbitalType
    {
        [InspectorName("HOMO-2")] HomoMinusTwo,
        [InspectorName("HOMO-1")] HomoMinusOne,
        [InspectorName("HOMO")] Homo,
        [InspectorName("LUMO")] Lumo,
        [InspectorName("LUMO+1")] LumoPlusOne,
        [InspectorName("LUMO+2")] LumoPlusTwo,
    }

    public enum Direction
    {
        [InspectorName("up")] D_up,
        [InspectorName("down")] D_down,
        [InspectorName("left")] D_left,
        [InspectorName("right")] D_right,
        [InspectorName("forward")] D_forward,
        [InspectorName("back")] D_back,
    }

    public enum DrawFunction
    {
        Draw,
        Erase,
    }

    public enum QuestionGoal
    {
        Extract,
        Represent,
        Predict,
    }

    public enum AtomLabel
    {
        Backbone,
        DieneEnd,
        Dienophile,
    }

    public enum HTMLColor
    {
        [StringValue("#00FFFF")] Aqua,
        [StringValue("#7FFFD4")] Aquamarine,
        [StringValue("#000000")] Black,
        [StringValue("#0000FF")] Blue,
        [StringValue("#8A2BE2")] BlueViolet,
        [StringValue("#A52A2A")] Brown,
        [StringValue("#FF7F50")] Coral,
        [StringValue("#DC143C")] Crimson,
        [StringValue("#00FFFF")] Cyan,
        [StringValue("#B8860B")] DarkGold,
        [StringValue("#FF00FF")] Fuchsia,
        [StringValue("#808080")] Gray,
        [StringValue("#008000")] Green,
        [StringValue("#F0E68C")] Khaki,
        [StringValue("#800000")] Maroon,
        [StringValue("#FFA500")] Orange,
        [StringValue("#FF4500")] OrangeRed,
        [StringValue("#800080")] Purple,
        [StringValue("#FF0000")] Red,
        [StringValue("#FA8072")] Salmon,
        [StringValue("#FFFFFF")] White,
        [StringValue("#FFFF00")] Yellow,
        [StringValue("#F7F7F7")] LightGray,
    }

    public enum VertexPlacement
    {
        None,
        TopInsideFront,
        TopInsideBack,
        TopOutsideFront,
        TopOutsideBack,
        BottomInsideFront,
        BottomInsideBack,
        BottomOutsideFront,
        BottomOutsideBack,
    }

    public enum CartesianAxis
    {
        x,
        y,
        z,
    }

    public enum ControllerRef
    {
        right,
        left,
    }
}
