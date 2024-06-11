using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        int Create(GroupCreateDto createDto);
        List<GroupGetDto> GetAll(string? serach=null);
        GroupGetDto GetById(int id);
        void Update(GroupUpdateDto updateDto,int Id);
        void Delete(int id);
    }
}
