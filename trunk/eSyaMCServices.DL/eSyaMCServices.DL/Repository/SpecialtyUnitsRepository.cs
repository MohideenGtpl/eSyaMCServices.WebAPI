using System;
using System.Collections.Generic;
using System.Text;
using eSyaMCServices.DL.Entities;
using eSyaMCServices.IF;
using eSyaMCServices.DO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eSyaMCServices.DL.Repository
{
   public class SpecialtyUnitsRepository : ISpecialtyUnitsRepository
    {
        public List<DO_SpecialtUnits> GetSpecialtyUnitsbyBusinessKey(int Businesskey)
        {
            try
            {

                List<DO_SpecialtUnits> Spunits_list = new List<DO_SpecialtUnits>();

                using (var db = new eSyaEnterprise())
                {

                    var sp_units = db.GtEsspbl.Where(c => c.BusinessKey== Businesskey).Join(db.GtEsspcd,
                          x => x.SpecialtyId,
                          y => y.SpecialtyId,
                         (x, y) => new DO_SpecialtUnits

                         {
                             SpecialtyId = x.SpecialtyId,
                             SpecialtyDesc = y.SpecialtyDesc
                         }).ToList();

                    foreach (var sp in sp_units)
                    {
                        DO_SpecialtUnits objsp;
                        var obj = db.GtEsspmc.Where(x => x.SpecialtyId== sp.SpecialtyId&& x.BusinessKey == Businesskey).OrderByDescending(x => x.EffectiveFrom).FirstOrDefault();
                        if (obj != null)
                        {
                            objsp = new DO_SpecialtUnits()
                            {
                                BusinessKey = obj.BusinessKey,
                                SpecialtyId = obj.SpecialtyId,
                                EffectiveFrom = obj.EffectiveFrom,
                                NoOfUnits = obj.NoOfUnits,
                                NewPatient = obj.NewPatient,
                                RepeatPatient = obj.RepeatPatient,
                                NoOfMaleBeds = obj.NoOfMaleBeds,
                                NoOfFemaleBeds = obj.NoOfFemaleBeds,
                                NoOfCommonBeds = obj.NoOfCommonBeds,
                                MaxStayAllowed=obj.MaxStayAllowed,
                                SpecialtyDesc=sp.SpecialtyDesc,
                                ActiveStatus = obj.ActiveStatus,
                            };
                        }
                        else
                        {
                            objsp = new DO_SpecialtUnits()
                            {
                                BusinessKey = 0,
                                SpecialtyId = sp.SpecialtyId,
                                SpecialtyDesc = sp.SpecialtyDesc,
                                EffectiveFrom = DateTime.Now,
                                ActiveStatus = true
                            };
                        }
                        Spunits_list.Add(objsp);
                    }
                }
                var Distinctspecialties = Spunits_list.GroupBy(x => x.SpecialtyId).Select(y => y.First());
                return Distinctspecialties.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertOrUpdateSpecialtyUnits(DO_SpecialtUnits obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        var spe_units = db.GtEsspmc.Where(s => s.SpecialtyId == obj.SpecialtyId && s.EffectiveFrom.Date == obj.EffectiveFrom.Date && s.BusinessKey == obj.BusinessKey).FirstOrDefault();
                        if (spe_units == null)
                        {
                            var obj_sp = new GtEsspmc
                            {
                                BusinessKey = obj.BusinessKey,
                                SpecialtyId = obj.SpecialtyId,
                                EffectiveFrom = obj.EffectiveFrom,
                                NoOfUnits = obj.NoOfUnits,
                                NewPatient = obj.NewPatient,
                                RepeatPatient = obj.RepeatPatient,
                                NoOfMaleBeds = obj.NoOfMaleBeds,
                                NoOfFemaleBeds = obj.NoOfFemaleBeds,
                                NoOfCommonBeds = obj.NoOfCommonBeds,
                                MaxStayAllowed = obj.MaxStayAllowed,
                                ActiveStatus = obj.ActiveStatus,
                                FormId=obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID
                            };
                            db.GtEsspmc.Add(obj_sp);
                        }
                        else
                        {
                                spe_units.NoOfUnits = obj.NoOfUnits;
                                spe_units.NewPatient = obj.NewPatient;
                                spe_units.RepeatPatient = obj.RepeatPatient;
                                spe_units.NoOfMaleBeds = obj.NoOfMaleBeds;
                                spe_units.NoOfFemaleBeds = obj.NoOfFemaleBeds;
                                spe_units.NoOfCommonBeds = obj.NoOfCommonBeds;
                                spe_units.MaxStayAllowed = obj.MaxStayAllowed;
                                spe_units.ActiveStatus = obj.ActiveStatus;
                                spe_units.ActiveStatus = obj.ActiveStatus;
                                spe_units.ModifiedBy = obj.UserID;
                                spe_units.ModifiedOn = System.DateTime.Now;
                                spe_units.ModifiedTerminal = obj.TerminalID;
                        }
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, Message = "Updated Successfully." };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
