using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVLAdminPanel.BLL.Services.Interfaces
{
    interface IServiceCRUD<T, I>
    {
        List<T> FindAll(string token);
    }
}
