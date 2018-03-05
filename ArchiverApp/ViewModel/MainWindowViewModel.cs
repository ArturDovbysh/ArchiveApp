using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WinForms = System.Windows.Forms;
using ArchiverApp.Command;
using Infrastructure.DataModels;
using Infrastructure.Services.FolderReader;
using Infrastructure.Services.FolderBinarySerializer;
using Infrastructure.Services.FolderWriter;

namespace ArchiverApp.ViewModel
{
    /// <summary>
    /// ViewModel of MainWindow view.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private RelayCommand _archive;
        private RelayCommand _unarchive;

        private string _archiveFolderPath = "empty";

        public string ArchiveFolderPath
        {
            get
            {
                return _archiveFolderPath;
            }

            set
            {
                _archiveFolderPath = value;
            }
        }

        public ICommand Archive
        {
            get
            {
                if (_archive == null)
                    _archive = new RelayCommand(ExecuteArchiveCommand, CanExecuteArchiveCommand);
                return _archive;
            }
        }

        public async void ExecuteArchiveCommand(object parameter)
        {
            try
            {
                using (var folderBrowserDialog = new WinForms.FolderBrowserDialog())
                {
                    if (folderBrowserDialog.ShowDialog() == WinForms.DialogResult.OK)
                    {
                        _archiveFolderPath = folderBrowserDialog.SelectedPath;
                    }
                }
                OnPropertyChanged("ArchiveFolderPath");

                WinForms.MessageBox.Show("Select where to save the file");

                IFolderReader folderReader = new FolderReader();

                string fileName = "";
                using (var fileDialog = new WinForms.SaveFileDialog())
                {
                    if (fileDialog.ShowDialog() == WinForms.DialogResult.OK)
                    {
                        fileName = string.Concat(fileDialog.FileName, ".dat");
                    }
                }

                Task<bool> t = Task<bool>.Run(() =>
                {

                    try
                    {
                        Folder folder = folderReader.GetFolders(_archiveFolderPath);

                        var fbs = new FolderBinarySerializer();

                        if (fbs.Serialize(folder, fileName))
                            WinForms.MessageBox.Show($"Folder : {_archiveFolderPath} was archivated successfully to file : {fileName}", "Archive info", WinForms.MessageBoxButtons.OK, WinForms.MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    return true;
                });

                await Task.WhenAll(t);
            }
            catch (Exception ex)
            {
                WinForms.MessageBox.Show($"Error in {ex.TargetSite}. \nError message : {ex.Message}", "Error!", WinForms.MessageBoxButtons.OK, WinForms.MessageBoxIcon.Error);
            }
        }

        public bool CanExecuteArchiveCommand(object parameter)
        {
            if (string.IsNullOrEmpty(_archiveFolderPath) || string.IsNullOrWhiteSpace(_archiveFolderPath))
                return false;
            return true;
        }

        public ICommand Unarchive
        {
            get
            {
                if (_unarchive == null)
                    _unarchive = new RelayCommand(ExecuteUnarchiveCommand, CanExecuteArchiveCommand);
                return _unarchive;
            }
        }

        public async void ExecuteUnarchiveCommand(object parameter)
        {
            try
            {
                using (var fileDialog = new WinForms.OpenFileDialog())
                {
                    if (fileDialog.ShowDialog() == WinForms.DialogResult.OK)
                    {
                        _archiveFolderPath = fileDialog.FileName;
                    }
                }
                OnPropertyChanged("ArchiveFolderPath");

                WinForms.MessageBox.Show("Select where to unarchive the file");

                string folderName = "";
                using (var folderBrowserDialog = new WinForms.FolderBrowserDialog())
                {
                    if (folderBrowserDialog.ShowDialog() == WinForms.DialogResult.OK)
                    {
                        folderName = folderBrowserDialog.SelectedPath;
                    }
                }

                Task<bool> t = Task<bool>.Run(() =>
                {
                    var fbs = new FolderBinarySerializer();

                    Folder newFolder = (Folder)fbs.Deserialize(_archiveFolderPath);

                    var folderWriter = new FolderWriter();
                    if (folderWriter.WriteFolder(newFolder, folderName))
                        WinForms.MessageBox.Show($"Folder {newFolder.Name} was unarchivated from file {_archiveFolderPath}", "Unarchive info", WinForms.MessageBoxButtons.OK, WinForms.MessageBoxIcon.Information);


                    return true;
                });

                await Task.WhenAll(t);
               
            }
            catch (Exception ex)
            {
                WinForms.MessageBox.Show($"Error in {ex.TargetSite}. \nError message : {ex.Message}", "Error!", WinForms.MessageBoxButtons.OK, WinForms.MessageBoxIcon.Error);
            }
        }
    }

}
