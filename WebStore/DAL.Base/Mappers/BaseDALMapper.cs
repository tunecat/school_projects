﻿using AutoMapper;
 using AutoMapper.Configuration;
 using Contracts.DAL.Base.Mappers;

namespace DAL.Base.Mappers
{
    public class BaseDALMapper<TLeftObject, TRightObject> : IBaseDALMapper<TLeftObject, TRightObject>
        where TLeftObject : class, new() 
        where TRightObject : class, new()

    {
        protected IMapper Mapper;
        protected readonly MapperConfigurationExpression MapperConfigurationExpression;

        public BaseDALMapper()
        {
            MapperConfigurationExpression = new MapperConfigurationExpression();
            MapperConfigurationExpression.CreateMap<TLeftObject, TRightObject>();
            MapperConfigurationExpression.CreateMap<TRightObject, TLeftObject>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));

        }

        public TRightObject Map(TLeftObject inObject)
        {
            return Mapper.Map<TLeftObject, TRightObject>(inObject);
        }
        
        public TLeftObject Map(TRightObject inObject)
        {
            return Mapper.Map<TRightObject, TLeftObject>(inObject);
        }
    }
}
