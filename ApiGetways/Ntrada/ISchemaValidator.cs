using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ntrada
{
    internal interface ISchemaValidator
    {
        Task<IEnumerable<Error>> ValidateAsync(string payload, string schema);
    }
}