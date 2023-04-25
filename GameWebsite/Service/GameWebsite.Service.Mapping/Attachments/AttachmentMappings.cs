using GameWebsite.Data.Models;
using GameWebsite.Service.Mapping.Users;
using GameWebsite.Service.Models.Attachments;

namespace GameWebsite.Service.Mapping.Attachments
{
    public static class AttachmentMappings
    {
        public static Attachment ToEntity(AttachmentDto attachmentDto)
        {
            return new Attachment
            {
                Id = attachmentDto.Id,
                CreatedAt = attachmentDto.CreatedAt,
                CreatedBy = attachmentDto.CreatedBy.ToEntity(),
                FileUrl = attachmentDto.FileUrl,
                FileName = attachmentDto.FileName
            };
        }

        public static AttachmentDto ToDto(Attachment attachment)
        {
            return new AttachmentDto
            {
                Id = attachment.Id,
                CreatedAt = attachment.CreatedAt,
                CreatedBy = attachment.CreatedBy.ToDto(),
                FileUrl = attachment.FileUrl,
                FileName = attachment.FileName
            };
        }
    }
}
