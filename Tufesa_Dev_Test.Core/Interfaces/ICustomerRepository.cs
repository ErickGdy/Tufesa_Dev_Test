using Tufesa_Dev_Test.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Tufesa_Dev_Test.Core.Interfaces
{
    public interface ICustomerRepository
    {
        #region CREATES
        Task<ActionResult> Create(Customer Customer);
        #endregion

        #region READS
        Task<ActionResult> Read(string id);
        Task<ActionResult> ReadAll();
        Task<ActionResult> ReadAllSortByName();
        #endregion

        #region UPDATES
        Task<ActionResult> Update(int id, Customer Customer);
        #endregion

        #region DELETES
        Task<ActionResult> Delete(string id);
        #endregion

    }
}
