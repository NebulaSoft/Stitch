public interface INoNameSpace
{
}

public class IAmAClass
{
}

namespace Scenarios
{
    public interface IHaveAMethod
    {
        void A();
    }

    public interface IReadonlyContract
    {
        int Int { get; }

        uint UInt { get; }

        string String { get; }

        float Float { get; }
    }

    public interface IReadWriteContract
    {
        int Int { get; set; }

        uint UInt { get; set; }

        string String { get; set; }

        float Float { get; set; }
    }

    public interface IEmpty
    {
    }

    public interface IInsideNamespace : IReadonlyContract
    {
    }

    public class NestedClass
    {
        public interface IInsideClass
        {
        }

        public interface IInsideClassInheriting : IReadonlyContract
        {
        }
    }
}
