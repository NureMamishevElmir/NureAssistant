using DomainEntity.AIEntities;
using DomainEntity.FileEntities;
using DomainEntity.Request;
using Repository.Repositories.Interfaces;

namespace NureAssistant.Services;

public interface IAskGraphService
{
    Task EnsureGraphAsync(MessageRequest request, CancellationToken cancellationToken = default);
}

public class AskGraphService : IAskGraphService
{
    private readonly IAIRepository _aiRepository;
    private readonly INureFileRepository _fileRepository;
    private readonly IAIFileRepository _aiFileRepository;
    private readonly IAIFileChatRepository _aiFileChatRepository;

    public AskGraphService(
        IAIRepository aiRepository,
        INureFileRepository fileRepository,
        IAIFileRepository aiFileRepository,
        IAIFileChatRepository aiFileChatRepository)
    {
        _aiRepository = aiRepository;
        _fileRepository = fileRepository;
        _aiFileRepository = aiFileRepository;
        _aiFileChatRepository = aiFileChatRepository;
    }

    public async Task EnsureGraphAsync(MessageRequest request, CancellationToken cancellationToken = default)
    {
        if (request.CustomerId == Guid.Empty)
        {
            return;
        }

        var now = DateTime.UtcNow;

        var ai = await _aiRepository.FirstOrDefaultAsync(
            a => a.CustomerId == request.CustomerId, cancellationToken);
        if (ai is null)
        {
            ai = new AI
            {
                Name = "NURE Assistant",
                IsActive = true,
                ActivatedAt = now,
                CustomerId = request.CustomerId,
            };
            await _aiRepository.AddAsync(ai, cancellationToken);
            await _aiRepository.SaveChangesAsync(cancellationToken);
        }

        if (request.FileId == Guid.Empty)
        {
            return;
        }

        var fileKey = request.FileId.ToString();
        var file = await _fileRepository.FirstOrDefaultAsync(
            f => f.Name == fileKey, cancellationToken);
        if (file is null)
        {
            file = new NureFile
            {
                Name = fileKey,
                Weight = "0",
                IsActive = true,
                ActivatedAt = now,
                CustomerId = request.CustomerId,
            };
            await _fileRepository.AddAsync(file, cancellationToken);
            await _fileRepository.SaveChangesAsync(cancellationToken);
        }

        var aiFile = await _aiFileRepository.FirstOrDefaultAsync(
            af => af.AIId == ai.Id && af.FileId == file.Id, cancellationToken);
        if (aiFile is null)
        {
            aiFile = new AIFile { AIId = ai.Id, FileId = file.Id };
            await _aiFileRepository.AddAsync(aiFile, cancellationToken);
            await _aiFileRepository.SaveChangesAsync(cancellationToken);
        }

        if (request.ChatId != Guid.Empty)
        {
            var chatLink = await _aiFileChatRepository.FirstOrDefaultAsync(
                c => c.ChatId == request.ChatId && c.AIFileId == aiFile.Id, cancellationToken);
            if (chatLink is null)
            {
                await _aiFileChatRepository.AddAsync(
                    new AIFileChat { ChatId = request.ChatId, AIFileId = aiFile.Id }, cancellationToken);
                await _aiFileChatRepository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
