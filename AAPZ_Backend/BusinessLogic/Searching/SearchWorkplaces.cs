using System.Collections.Generic;
using System.Linq;
using AAPZ_Backend.Repositories;
using AAPZ_Backend.Models;


namespace AAPZ_Backend.BusinessLogic.Searching
{

    public class BuildingSearchingResult
    {
        public Building Building { get; set; }
        public List<WorkplaceSearchingResult> WorkplaceSearchingResults { get; set; } = new List<WorkplaceSearchingResult>();
    }

    public class WorkplaceSearchingResult
    {
        public int WorkplaceId { get; set; }
        public double EquipmentAppropriation { get; set; }
        public double CostAppropriation { get; set; }
    }

    public class SearchWorkplaces
    {
        private readonly IDBActions<WorkplaceParameter> _workplaceParameterDB;
        private readonly IDBActions<SearchSetting> _searchSettingDB;
        private readonly BuildingRepository _buildingDB;
        private readonly WorkplaceRepository _workplaceDB;
        private readonly WorkplaceEquipmentRepository _workplaceEquipmentDB;
        private readonly double _idealMark;

        private readonly double _latitude;
        private readonly double _longitude;
        private readonly IEnumerable<WorkplaceParameter> _workplaceParameters;
        private readonly SearchSetting _searchSetting;
        private readonly IEnumerable<Building> _buildingsInRadius;


        public SearchWorkplaces(double latitude, double longitude, int clientId)
        {
            _workplaceParameterDB = new WorkplaceParameterRepository();
            _searchSettingDB = new SearchSettingRepository();
            _buildingDB = new BuildingRepository();
            _workplaceDB = new WorkplaceRepository();
            _workplaceEquipmentDB = new WorkplaceEquipmentRepository();
            _latitude = latitude;
            _longitude = longitude;
            _workplaceParameters = _workplaceParameterDB.GetEntityListByClientId(clientId);
            _idealMark = GetIdealMark();
            _searchSetting = _searchSettingDB.GetEntity(clientId);
            _buildingsInRadius = GetBuildingsInRadius();
        }

        public SearchWorkplaces(int clientId)
        {
            _workplaceParameterDB = new WorkplaceParameterRepository();
            _searchSettingDB = new SearchSettingRepository();
            _buildingDB = new BuildingRepository();
            _workplaceDB = new WorkplaceRepository();
            _workplaceEquipmentDB = new WorkplaceEquipmentRepository();
                        _workplaceParameters = _workplaceParameterDB.GetEntityListByClientId(clientId);
            _idealMark = GetIdealMark();
            _searchSetting = _searchSettingDB.GetEntity(clientId);
        }

        private double GetIdealMark()
        {
            double idealMark = 0;
            foreach (var workplaceParameter in _workplaceParameters)
            {
                idealMark += workplaceParameter.Priority;
            }

            return idealMark;
        }

        private IEnumerable<Building> GetBuildingsInRadius()
        {
            double degreeRadius = _searchSetting.Radius / 111;
            return _buildingDB.GetBuildingsInRadius(_latitude + degreeRadius,
                _latitude - degreeRadius, _longitude + degreeRadius, _longitude - degreeRadius);
        }

        public List<BuildingSearchingResult> GetSearchingResult()
        {
            List<BuildingSearchingResult> searchingResult = new List<BuildingSearchingResult>();

            foreach (var buildingsInRadius in _buildingsInRadius)
            {
                searchingResult.Add(GetBuildingSearchingResult(buildingsInRadius));
            }

            return searchingResult;
        }

        public BuildingSearchingResult GetAppropriationByBuildingResults(long buildingId)
        {
            Building building = _buildingDB.GetEntity(buildingId);
            return GetBuildingSearchingResult(building);
        }

        private BuildingSearchingResult GetBuildingSearchingResult(Building building)
        {
            BuildingSearchingResult buildingSearchingResult = new BuildingSearchingResult();
            buildingSearchingResult.Building = building;

            IEnumerable<Workplace> buildingWorkplaces = _workplaceDB.GetWorkplacesByBuildingId(building.Id);

            foreach (var buildingWorkplace in buildingWorkplaces)
            {
                WorkplaceSearchingResult workplaceSearchingResult = new WorkplaceSearchingResult();
                workplaceSearchingResult.WorkplaceId = buildingWorkplace.Id;

                IEnumerable<WorkplaceEquipment> currentWorkplaceEquipments =
                    _workplaceEquipmentDB.GetWorkplaceEquipmentByWorkplace(buildingWorkplace.Id);


                double equipmentAppropriation = 0;

                foreach (var workplaceParameter in _workplaceParameters)
                {
                    WorkplaceEquipment currentWorkplaceEquipment =
                        currentWorkplaceEquipments.FirstOrDefault(x => x.EquipmentId == workplaceParameter.EquipmentId);

                    if (currentWorkplaceEquipment != null && workplaceParameter.Priority != 0)
                    {
                        if (workplaceParameter.Count == 0)
                        {
                            equipmentAppropriation += workplaceParameter.Priority;
                        }
                        else
                        {
                            double workplaceSatisfactionCoefficient =
                                (double) currentWorkplaceEquipment.Count / (double) workplaceParameter.Count;
                            workplaceSatisfactionCoefficient = (workplaceSatisfactionCoefficient > 1) ? 1 : workplaceSatisfactionCoefficient;

                            equipmentAppropriation += (workplaceSatisfactionCoefficient * workplaceParameter.Priority);
                        }
                    }
                }

                if (!_workplaceParameters.Any() || _idealMark == 0)
                {
                    workplaceSearchingResult.EquipmentAppropriation = 100;
                }
                else
                {
                    workplaceSearchingResult.EquipmentAppropriation = equipmentAppropriation / _idealMark * 100;
                }

                if (buildingWorkplace.Cost == 0)
                {
                    workplaceSearchingResult.CostAppropriation = 100;
                }
                else
                {
                    double costStisfactionCoefficient =
                        (double) _searchSetting.WantedCost / (double) buildingWorkplace.Cost;
                    costStisfactionCoefficient = (costStisfactionCoefficient > 1) ? 1 : costStisfactionCoefficient;

                    workplaceSearchingResult.CostAppropriation = costStisfactionCoefficient * 100;
                }

                buildingSearchingResult.WorkplaceSearchingResults.Add(workplaceSearchingResult);
            }

            return buildingSearchingResult;
        }
    }
}
