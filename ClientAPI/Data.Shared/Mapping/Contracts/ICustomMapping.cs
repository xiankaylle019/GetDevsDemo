using AutoMapper;

namespace ClientAPI.Data.Shared.Mapping.Contracts
{
    public interface ICustomMapping
    {
         void CreateMapping(IMapperConfigurationExpression cfg);
    }
}