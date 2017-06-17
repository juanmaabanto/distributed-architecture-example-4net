using AutoMapper;
using CatSolution.Application.Core;
using CatSolution.Application.MainModule.Adapters;
using CatSolution.CrossCutting.Logging.LoggerEvent;
using CatSolution.Domain.Core;
using CatSolution.Domain.MainModule.Contracts.Options;
using CatSolution.Domain.MainModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatSolution.Application.MainModule.Services.Option
{
    public class OptionManagementService : ManagementService<SYS_Option, SYS_OptionDTO>, IOptionManagementService
    {
        Logger _log = null;
        IOptionRepository _OptionRepository;

        public OptionManagementService(IOptionRepository optionRepository) :base(optionRepository)
        {
            Mapper.Initialize(cfg => { cfg.CreateMap<SYS_Option, SYS_OptionDTO>();
                cfg.CreateMap<SYS_DetailOption, SYS_DetailOptionDTO>();
            });
            _log = new Logger();
            _OptionRepository = optionRepository;   
        }

        public SYS_OptionDTO ModifyOption(SYS_Option item)
        {
            IUnitOfWork unitOfWork = _OptionRepository.UnitOfWork;
            SYS_OptionDTO entityDTO = null;
            

            try
            {
                var origin = _OptionRepository.GetById(item.OptionId);

                if (origin == null)
                {
                    throw new ArgumentNullException("origin");
                }

                origin.ApplicationId = item.ApplicationId;
                origin.ParentId = item.ParentId;
                origin.Name = item.Name;
                origin.Tooltip = item.Tooltip;
                origin.Sequence = item.Sequence;
                origin.ViewClass = item.ViewClass;
                origin.ViewType = item.ViewType;
                origin.Icon = item.Icon;
                origin.Leaf = item.Leaf;
                origin.Active = item.Active;
                origin.ApplicationName = item.ApplicationName;
                origin.ParentName = item.ParentName;

                var entities = (from i in item.SYS_DetailOption
                                join o in origin.SYS_DetailOption on i.DetailOptionId equals o.DetailOptionId into records
                                from r in records.DefaultIfEmpty()
                                select new { current = i, old = r }).ToList();

                foreach (var entity in entities)
                {
                    if ( entity.current.DetailOptionId == 0 )
                    {
                        origin.SYS_DetailOption.Add(entity.current);
                    }
                    else
                    {
                        if ( entity.old != null )
                        {
                            entity.old.Name = entity.current.Name;
                            entity.old.ControllerName = entity.current.ControllerName;
                            entity.old.ActionName = entity.current.ActionName;
                            entity.old.Active = entity.current.Active;
                        }
                    }
                }

                var deletes = (from o in origin.SYS_DetailOption
                               join i in item.SYS_DetailOption on o.DetailOptionId equals i.DetailOptionId into records
                               from r in records.DefaultIfEmpty()
                               where r == null
                               select new { old = o }).ToList();//.ForEach(d => { if (d.delete == null) _OptionRepository.RemoveDetailOption(d.o); else { } });

                foreach (var delete in deletes)
                {
                    _OptionRepository.RemoveDetailOption(delete.old);
                }

                _OptionRepository.Modify(origin);
                unitOfWork.Commit();
                entityDTO = Mapper.Map<SYS_OptionDTO>(origin);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return entityDTO;
        }
    }
}
