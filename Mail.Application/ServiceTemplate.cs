using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain = Pagoefectivo.Mail.Domain;
using Dynamo = Mail.Infraestructure.DynamoRepository;
using S3 = Mail.Infraestructure.S3Repository;
namespace Mail.Application
{
    public class ServiceTemplate
    {

        public static bool ExistTemplate(int id)
        {
            var baseRepository = new Dynamo.BaseRepository();
            var exists = baseRepository.GetItem<Dynamo.Template>(id) != null;
            return exists;
        }

        public static Domain.Template GetTemplate(int id)
        {
            Dynamo.BaseRepository baseRepository = new Dynamo.BaseRepository();
            var template = baseRepository.GetItem<Dynamo.Template>(id);
            return new Domain.Template() { IdTemplate = template.IdTemplate, Bucket = template.path, FileName = template.FileName };
        }


        public static string ToString(Domain.Template template)
        {
            var bucketName = string.Empty;

            if (template.path != null && template.path.Trim() != string.Empty)
                bucketName += ValidateSlash(template.Bucket) + template.path;
            else
                bucketName = template.Bucket;

            if (!string.IsNullOrEmpty(template.Language))
                template.FileName = LanguageTemplate(template.Language, template.FileName);

            var templateString = new S3.BaseRepository().GetObjectDataToString(bucketName, template.FileName);
            return templateString;
        }


        private static string ValidateSlash(string route)
        {
            if (route.LastIndexOf('/') != route.Length - 1)
                route += '/';
            return route;
        }

        private static string LanguageTemplate(string language, string fileNameTemplate)
        {
            var extent = fileNameTemplate.Substring(fileNameTemplate.LastIndexOf('.'));
            fileNameTemplate = fileNameTemplate.Substring(0, fileNameTemplate.LastIndexOf('.'));
            fileNameTemplate += "-" + language + extent;
            return fileNameTemplate;
        }

    }
}
