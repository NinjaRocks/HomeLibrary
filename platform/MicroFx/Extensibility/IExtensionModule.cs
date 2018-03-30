using System;

namespace MicroFx.Extensibility
{
    public interface IExtensionModule
    {
        void Init(Func<IRegisterContext, bool> nextMiddleware);
        bool Register(IRegisterContext context);
    }
}