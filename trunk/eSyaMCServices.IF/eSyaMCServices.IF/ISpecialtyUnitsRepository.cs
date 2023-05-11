using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using eSyaMCServices.DO;
namespace eSyaMCServices.IF
{
   public interface ISpecialtyUnitsRepository
    {
        List<DO_SpecialtUnits> GetSpecialtyUnitsbyBusinessKey(int Businesskey);
        Task<DO_ReturnParameter> InsertOrUpdateSpecialtyUnits(DO_SpecialtUnits obj);
    }
}
