using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.Interfaces;

public interface IJwtService
{
    string GenerateAdminToken(int adminId, string code);
    string GenerateTeacherToken(int teacherId, string codeNumber, string teacherType);
}