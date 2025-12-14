using NLog;
using WindowsDb2Editor.Services.AI.Export;
using Xunit;

namespace WindowsDb2Editor.AutoTests;

/// <summary>
/// Tests for AI Export services
/// </summary>
public class ExportServiceTests
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    [Fact]
    public void AiExportService_CanBeCreated()
    {
        // Arrange & Act
        var service = new AiExportService();

        // Assert
        Assert.NotNull(service);
        Logger.Info("✅ AiExportService created successfully");
    }

    [Fact]
    public void ExternalEditorService_CanBeCreated()
    {
        // Arrange & Act
        var service = new ExternalEditorService();

        // Assert
        Assert.NotNull(service);
        Logger.Info("✅ ExternalEditorService created successfully");
    }
}

