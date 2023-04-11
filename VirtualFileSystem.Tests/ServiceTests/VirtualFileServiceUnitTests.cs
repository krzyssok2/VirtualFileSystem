using Moq;
using VirtualFileSystem.Constants;
using VirtualFileSystem.Interfaces;
using VirtualFileSystem.Services;
using VirtualFileSystem.Tests.Arrange;

namespace VirtualFileSystem.Tests.ServiceTests;

public class VirtualFileServiceUnitTests
{
    private Mock<IStorageService> _mockStorageService;

    [SetUp]
    public void Setup()
    {
        _mockStorageService = new Mock<IStorageService>();
    }

    [Test]
    public void RemoveFile_Failure_FileNotFound()
    {
        _mockStorageService.Setup(i => i.GetSystemData()).Returns(ArrangeVirtualFileServiceUnitTests.RootFolderWithParent1AndNoFiles);

        var testingObject = InitializeTestingObject();

        var result = testingObject.RemoveFile(new string[] { "Parents1" }, "test.exe");

        Assert.That(result, Is.EqualTo(ApplicationConstants.FileNotFound));
    }

    [Test]
    public void RemoveFile_Failure_SpecifiedFolderNotFound()
    {
        _mockStorageService.Setup(i => i.GetSystemData()).Returns(ArrangeVirtualFileServiceUnitTests.RootFolderWithNoFolders);

        var testingObject = InitializeTestingObject();

        var result = testingObject.RemoveFile(new string[] { "Parents1" }, "test.exe");

        Assert.That(result, Is.EqualTo(ApplicationConstants.FolderNotFoundForSpecifiedFile));
    }

    [Test]
    public void AddFile_Failure_FileWithSameNameExists()
    {
        _mockStorageService.Setup(i => i.GetSystemData()).Returns(ArrangeVirtualFileServiceUnitTests.RootFolderWithParent1FolderAndTestExeFile);

        var testingObject = InitializeTestingObject();

        var result = testingObject.AddFile(new string[] { "Parents1" }, "test.exe");

        Assert.That(result, Is.EqualTo(ApplicationConstants.FileAlreadyExists));
    }

    [Test]
    public void AddFile_Success_FileAdded()
    {
        _mockStorageService.Setup(i => i.GetSystemData()).Returns(ArrangeVirtualFileServiceUnitTests.RootFolderWithParent1FolderAndTestExeFile);

        var testingObject = InitializeTestingObject();

        var result = testingObject.AddFile(new string[] { "Parents1" }, "test2.exe");

        Assert.That(result, Is.EqualTo(ApplicationConstants.SuccessFileAdded));
    }

    [Test]
    public void DeleteFolder_Failure_FolderNotFound()
    {
        _mockStorageService.Setup(i => i.GetSystemData()).Returns(ArrangeVirtualFileServiceUnitTests.RootFolderWithParent1FolderAndTestExeFile);

        var testingObject = InitializeTestingObject();

        var result = testingObject.DeleteFolder(new string[] { "Parents2" });

        Assert.That(result, Is.EqualTo(ApplicationConstants.FolderNotFound));
    }

    [Test]
    public void DeleteFolder_Success_FolderDeleted()
    {
        _mockStorageService.Setup(i => i.GetSystemData()).Returns(ArrangeVirtualFileServiceUnitTests.RootFolderWithParent1FolderAndTestExeFile);

        var testingObject = InitializeTestingObject();

        var result = testingObject.DeleteFolder(new string[] { "Parents1" });

        Assert.That(result, Is.EqualTo(ApplicationConstants.SuccessFolderDeleted));
    }

    private VirtualFileService InitializeTestingObject()
    {
        return new VirtualFileService(_mockStorageService.Object);
    }
}