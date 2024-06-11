using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IStudentService
    {
        int Create(StudentCreateDto createDto);
        List<StudentGetDto> GetAll(string? search = null);
        StudentGetDto GetById(int id);
        void Update(StudentUpdateDto updateDto, int Id);
        void Delete(int id);
    }
}
