using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(int adminId, string code);
}