using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using ClientAPI.Data.Shared.Mapping.Contracts;

namespace ClientAPI.Data.Shared.Mapping
{
    public class AutoMapperConfig
    {
         public static void Execute()
        {

            var types = Assembly.GetExecutingAssembly().GetExportedTypes().ToList(); 

            Mapper.Initialize(

                          cfg =>
                          {
                              
                              LoadStandardMappings(types, cfg);

                              LoadReverseMappings(types, cfg);

                              LoadCustomMappings(types, cfg);

                          }

                      );

        }
       

        private static void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(ICustomMapping).IsAssignableFrom(t) &&
                        !t.IsAbstract &&
                        !t.IsInterface 
                        select (ICustomMapping)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMapping(cfg);
            }
        }
        private static void LoadReverseMappings(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapDestination<>)
                              && !t.IsAbstract
                              && !t.IsInterface
                        select new
                        {
                            Source = t,
                            Destination = i.GetGenericArguments()[0]
                        }).ToList();
        
            //foreach (var map in maps)
            //{
            //    Mapper.CreateMap(map.Source, map.Destination);
            //}
            maps.ForEach(map =>
            {
                cfg.CreateMap(map.Source, map.Destination);
            });
        }
        private static void LoadStandardMappings(IEnumerable<Type> types, IMapperConfigurationExpression cfg)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IMapSource<>) &&
                        !t.IsAbstract &&
                        !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }
                       ).ToList();

            //Obsolete code in Automapper 5.0
            //foreach (var map in maps)
            //{
            //    Mapper.CreateMap(map.Source, map.Destination);
            //}

            maps.ForEach(map =>
            {
                cfg.CreateMap(map.Source, map.Destination);

            });

        }

    }
}