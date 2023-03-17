using Tufesa_Dev_Test.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Tufesa_Dev_Test.API.Interfaces
{
    public interface ICustomerController
    {
        #region CREATES
        Task<ActionResult> Create(string values);
        #endregion

        #region READS
        Task<ActionResult> Read(int id);
        Task<ActionResult> ReadAll();
        Task<ActionResult> ReadAllSortByName();
        #endregion

        #region UPDATES
        Task<ActionResult> Update(int id, string values);
        #endregion

        #region DELETES
        Task<ActionResult> Delete(int id);
        #endregion
    }
}
