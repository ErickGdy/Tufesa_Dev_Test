using Tufesa_Dev_Test.Core.Interfaces;
using Tufesa_Dev_Test.Core.Tools;
using Tufesa_Dev_Test.Data;
using Tufesa_Dev_Test.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tufesa_Dev_Test.Core.Repositories
{
    public class CustomerRepository : ICustomerRepository
    { 
        private readonly TufesaDbContext _context;

        public CustomerRepository(TufesaDbContext context)
        {
            _context = context;
        }
    
        public async Task<ActionResult> Create(Customer Customer)
        {
            try
            {
                if (!RFCValidation(Customer.RFC)) {
                    var response = new ObjectResult("RFC is invalid");
                    response.StatusCode = 400;
                    return response;
                }
                _context.Customers.Add(Customer);
                var result = await _context.SaveChangesAsync();
                return new OkObjectResult(Customer);
            }catch(Exception ex)
            {
                var response = new ObjectResult(ex.InnerException ==null ? ex.Message : ex.InnerException.Message);
                response.StatusCode = 500;
                return response;
            }
        }
        public async Task<ActionResult> Update(int id, Customer Customer)
        {
            try
            {
                Customer Auxobj = _context.Customers.FindAsync(id).Result;
                if (Auxobj == null)
                {
                    var response = new ObjectResult("Unable to find Customer.");
                    response.StatusCode = 404;
                    return response;
                }
                if (!RFCValidation(Customer.RFC))
                {
                    var response = new ObjectResult("RFC is invalid");
                    response.StatusCode = 400;
                    return response;
                }
                Reflection.CopyProperties(Customer, Auxobj);
                _context.Entry(Auxobj).State= EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OkObjectResult( Customer);
            }
            catch (Exception ex)
            {
                var response = new ObjectResult(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                response.StatusCode = 500;
                return response;
            }
        }

        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                Customer Auxobj = _context.Customers.FindAsync(Convert.ToInt32(id)).Result;
                if (Auxobj == null)
                {
                    var response = new ObjectResult("Unable to find Customer.");
                    response.StatusCode = 404;
                    return response;
                }
                Auxobj.Status = Auxobj.Status != Customer.CustomerStatus.Inactive ? Customer.CustomerStatus.Inactive : Customer.CustomerStatus.Active;
                await _context.SaveChangesAsync();
                return new OkObjectResult( Auxobj );
            }
            catch (Exception ex)
            {
                var response = new ObjectResult(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                response.StatusCode = 500;
                return response;
            }
        }

        public async Task<ActionResult> Read(string id)
        {
            try
            {
                Customer Auxobj = _context.Customers.FindAsync(Convert.ToInt32(id)).Result;
                if (Auxobj == null)
                {
                    var response = new ObjectResult("Unable to find Customer.");
                    response.StatusCode = 404;
                    return response;
                }
                
                return new OkObjectResult( Auxobj );
            }
            catch (Exception ex)
            {
                var response = new ObjectResult(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                response.StatusCode = 500;
                return response;
            }
        }

        public async Task<ActionResult> ReadAll()
        {
            try
            {
                List<Customer> Auxobj = _context.Customers.OrderBy(m=> m.BornDate).ToList();
                return new OkObjectResult( Auxobj );
            }
            catch (Exception ex)
            {
                var response = new ObjectResult(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                response.StatusCode = 500;
                return response;
            }
        }
        public async Task<ActionResult> ReadAllSortByName()
        {
            try
            {
                List<Customer> Auxobj = _context.Customers.OrderBy(m => (m.FirstName + m.LastName)).ToList();
                return new OkObjectResult(Auxobj);
            }
            catch (Exception ex)
            {
                var response = new ObjectResult(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                response.StatusCode = 500;
                return response;
            }
        }
        #region TOOLS
        private bool RFCValidation(string rfc)
        {
            Regex rx = new Regex(@"^(((?!(([CcKk][Aa][CcKkGg][AaOo])|([Bb][Uu][Ee][YyIi])|([Kk][Oo](([Gg][Ee])|([Jj][Oo])))|([Cc][Oo](([Gg][Ee])|([Jj][AaEeIiOo])))|([QqCcKk][Uu][Ll][Oo])|((([Ff][Ee])|([Jj][Oo])|([Pp][Uu]))[Tt][Oo])|([Rr][Uu][Ii][Nn])|([Gg][Uu][Ee][Yy])|((([Pp][Uu])|([Rr][Aa]))[Tt][Aa])|([Pp][Ee](([Dd][Oo])|([Dd][Aa])|([Nn][Ee])))|([Mm](([Aa][Mm][OoEe])|([Ee][Aa][SsRr])|([Ii][Oo][Nn])|([Uu][Ll][Aa])|([Ee][Oo][Nn])|([Oo][Cc][Oo])))))[A-Za-zñÑ&][aeiouAEIOUxX]?[A-Za-zñÑ&]{2}(((([02468][048])|([13579][26]))0229)|(\d{2})((02((0[1-9])|1\d|2[0-8]))|((((0[13456789])|1[012]))((0[1-9])|((1|2)\d)|30))|(((0[13578])|(1[02]))31)))[a-zA-Z1-9]{2}[\dAa])|([Xx][AaEe][Xx]{2}010101000))$",
         RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return rx.IsMatch(rfc);
        }

        private bool Exist(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
        #endregion

    }
}
