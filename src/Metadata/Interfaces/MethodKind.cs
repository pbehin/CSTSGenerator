namespace Typewriter.Metadata.Interfaces
{
    //
    // Summary:
    //     Enumeration for possible kinds of method symbols.
    public enum MethodKind
    {
        //
        // Summary:
        //     An anonymous method or lambda expression
        AnonymousFunction = 0,
        LambdaMethod = 0,
        //
        // Summary:
        //     Method is a constructor.
        Constructor = 1,
        //
        // Summary:
        //     Method is a conversion.
        Conversion = 2,
        //
        // Summary:
        //     Method is a delegate invoke.
        DelegateInvoke = 3,
        //
        // Summary:
        //     Method is a destructor.
        Destructor = 4,
        //
        // Summary:
        //     Method is an event add.
        EventAdd = 5,
        //
        // Summary:
        //     Method is an event raise.
        EventRaise = 6,
        //
        // Summary:
        //     Method is an event remove.
        EventRemove = 7,
        //
        // Summary:
        //     Method is an explicit interface implementation.
        ExplicitInterfaceImplementation = 8,
        //
        // Summary:
        //     Method is an operator.
        UserDefinedOperator = 9,
        //
        // Summary:
        //     Method is an ordinary method.
        Ordinary = 10,
        //
        // Summary:
        //     Method is a property get.
        PropertyGet = 11,
        //
        // Summary:
        //     Method is a property set.
        PropertySet = 12,
        //
        // Summary:
        //     An extension method with the "this" parameter removed.
        ReducedExtension = 13,
        //
        // Summary:
        //     Method is a static constructor.
        StaticConstructor = 14,
        SharedConstructor = 14,
        //
        // Summary:
        //     A built-in operator.
        BuiltinOperator = 15,
        //
        // Summary:
        //     Declare Sub or Function.
        DeclareMethod = 16
    }
}