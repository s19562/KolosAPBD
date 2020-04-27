using Kolokwium.DTOs;
using Kolokwium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium.Service
{
   public interface IDbService
    {
        public IEnumerable<Taski> GetTasks(int id);

        public string AddTask(AddTaskRequest request);

    }
}
