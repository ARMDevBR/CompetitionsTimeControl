using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionsTimeControl.Controllers
{
    internal class ConfigurationsDataController
    {
        public BeepsController DataBeepsController { get; set; } = null!;
        public MusicsController DataMusicsController { get; set; } = null!;
        public CompetitionController DataCompetitionController { get; set; } = null!;
    }
}
