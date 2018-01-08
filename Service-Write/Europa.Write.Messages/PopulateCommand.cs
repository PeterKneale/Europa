using System.Collections.Generic;
using Europa.Infrastructure;

namespace Europa.Write.Messages
{
    public class PopulateCommand : ICommand
    {
    }

    public class PopulateValidator : MessageValidator<PopulateCommand>
    {
    }

}
