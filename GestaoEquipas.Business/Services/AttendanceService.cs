using GestaoEquipas.Data.DataAccess;
using GestaoEquipas.Data.Models;
using System.Collections.Generic;

namespace GestaoEquipas.Business.Services
{
    public class AttendanceService
    {
        private readonly AttendanceRepository _repo = new AttendanceRepository();

        public void AddRecords(IEnumerable<AttendanceRecord> records) => _repo.AddRecords(records);

        public IEnumerable<AttendanceRecord> GetBySession(int sessionId) => _repo.GetBySession(sessionId);
    }
}
