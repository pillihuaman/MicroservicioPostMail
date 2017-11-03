using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynamo = Mail.Infraestructure.DynamoRepository;

using S3 = Mail.Infraestructure.S3Repository;


namespace PagoEfectivo.Mail.Application
{
    public class ServiceTemplate
    {

        public static bool ExistTemplate(int id)
        {
            var baseRepository = new Dynamo.BaseRepository();
            var exist = baseRepository.GetItem<Dynamo.Template>(id) != null;
            return exist;

        }

        public static Template GetTemplate(int id)
        {
            Dynamo.BaseRepository baseRepository = new Dynamo.BaseRepository();
            var template = baseRepository.GetItem<Dynamo.Template>(id);
            return new Template()
            {
                IdTemplate = template.IdTemplate,
                Bucket = template.path,
                FileName = template.FileName
            };

        }

        public static string ToString(Template template)
        {
            var bucketName = string.Empty;
            if (template.path != null && template.path.Trim() != string.Empty)
            {

                bucketName += ValidaSlash(template.Bucket) + template.path;
            }
            else
            {
                bucketName = template.Bucket;

            }
            if (!string.IsNullOrEmpty(template.Language))
                template.FileName = LanguageTemplate(template.Language, template.FileName);

            var templatestring = new S3.BaseRepository().GetObjectDataToString(bucketName, template.FileName);
            return templatestring;
        }

        private static string ValidaSlash(string bucket)
        {
            if (bucket.LastIndexOf('/') != bucket.Length - 1)
                bucket += '/';
            return bucket;
        }

        private static string LanguageTemplate(string languaje , string filename)
        {
            var extent = filename.Substring(filename.LastIndexOf('.'));
            filename += "-" + languaje + extent;
            return filename;

        }
    }
}
