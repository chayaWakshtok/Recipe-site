using DAL.Models.DB;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<CategoryDTO>>> GetAll();
        Task<ServiceResponse<List<CategoryDTO>>> Add(CategoryDTO newcategory);
        Task<ServiceResponse<List<CategoryDTO>>> Delete(int workId);
        Task<ServiceResponse<CategoryDTO>> GetOne(int id);
        Task<ServiceResponse<CategoryDTO>> Update(CategoryDTO categoryUpdate);

        //Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        //Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
        //Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter);
        //Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
        //Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);
    }
}
