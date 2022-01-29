using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using DevOpsInterview.Configuration;

namespace DevOpsInterview.Services
{
    public class CsvIoService
    {
        public IList<TClass> Load<TClass, TClassMap>(string filePath) where TClassMap : ClassMap<TClass>
        {
            using (var reader = new StreamReader(filePath))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = SharedConstants.CsvHasHeaderRecord,
                    Delimiter = SharedConstants.CsvDelimiter,
                };
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<TClassMap>();
                    return csv.GetRecords<TClass>().ToList();
                }
            }
        }
        
        public void Save<TClass, TClassMap>(string filePath, IEnumerable<TClass> records) where TClassMap : ClassMap<TClass>
        {
            using (var writer = new StreamWriter(filePath))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<TClassMap>();
                    csv.WriteRecords(records);
                }
            }
        }
    }
}