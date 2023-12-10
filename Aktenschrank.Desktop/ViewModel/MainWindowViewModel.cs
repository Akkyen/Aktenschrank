using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using Aktenschrank.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

// ReSharper disable InconsistentNaming

namespace Aktenschrank.Desktop.ViewModel;

public class MainWindowViewModel : ObservableObject
{
    private ObservableCollection<FileManagingProfile> _fileManagingProfiles = new();

    #region FmpOv_Commands

    public RelayCommand FmpOv_Create_Fmp_Command { get; }

    public RelayCommand FmpOv_Lb_SelectedItem_Delete_Command { get; }
    public RelayCommand FmpOv_Lb_SelectedItem_Duplicate_Command { get; }

    public RelayCommand<FileManagingProfile> FmpOv_Lb_BtDelete_Command { get; }

    #endregion

    #region FmpDt_Commands

    public RelayCommand FmpDt_CreateBehaviour_Command { get; }
    public RelayCommand FmpDt_DeleteBehaviour_Command { get; }
    public RelayCommand<Behaviour> FmpDt_LbBehaviours_BtDelete_Command { get; }

    public RelayCommand FmpDt_LbBehaviours_BtMoveUp_Command { get; }
    public RelayCommand FmpDt_LbBehaviours_BtMoveDown_Command { get; }


    public RelayCommand FmpDt_CreateTarget_Command { get; }
    public RelayCommand FmpDt_DeleteTarget_Command { get; }
    public RelayCommand<Target> FmpDt_LbTargets_BtDelete_Command { get; }

    public RelayCommand FmpDt_LbTargets_BtMoveUp_Command { get; }
    public RelayCommand FmpDt_LbTargets_BtMoveDown_Command { get; }

    #endregion

    #region UcBh_Commands

    public RelayCommand UcBh__Command { get; }

    #endregion

    #region UcTg_Commands

    public RelayCommand UcTg_SetFolderPath_Command { get; }

    #endregion

    #region Bindings_FileManagingProfile_Overview

    private string _spOv_TbName;
    private FileManagingProfile _spOv_LbSelectedItem;

    #region Properties

    public string FmpOv_TbName
    {
        get => _spOv_TbName;
        set
        {
            if (value == _spOv_TbName) return;
            _spOv_TbName = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public FileManagingProfile FmpOv_LbSelectedItem
    {
        get => _spOv_LbSelectedItem;
        set
        {
            if (Equals(value, _spOv_LbSelectedItem)) return;
            _spOv_LbSelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #endregion

    #region Bindings_FileManagingProfile_Details

    private Behaviour _spDt_LbBehaviourSelectedItem;
    private Target _spDt_LbTargetSelectedItem;

    #region Properties

    public Behaviour FmpDt_LbBehaviourSelectedItem
    {
        get => _spDt_LbBehaviourSelectedItem;
        set
        {
            if (Equals(value, _spDt_LbBehaviourSelectedItem)) return;
            _spDt_LbBehaviourSelectedItem = value;
            OnPropertyChanged();
        }
    }

    public Target FmpDt_LbTargetSelectedItem
    {
        get => _spDt_LbTargetSelectedItem;
        set
        {
            if (Equals(value, _spDt_LbTargetSelectedItem)) return;
            _spDt_LbTargetSelectedItem = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #endregion

    public MainWindowViewModel()
    {
        FmpOv_Create_Fmp_Command = new RelayCommand(FmpOv_BtCreate_Fmp);

        FmpOv_Lb_SelectedItem_Delete_Command = new RelayCommand(FmpOv_Lb_SelectedItem_Delete);
        FmpOv_Lb_SelectedItem_Duplicate_Command = new RelayCommand(FmpOv_Lb_SelectedItem_Duplicate);

        FmpOv_Lb_BtDelete_Command = new RelayCommand<FileManagingProfile>(FmpOv_Lb_BtDelete);



        FmpDt_CreateBehaviour_Command = new RelayCommand(FmpDt_CreateBehaviour);
        FmpDt_DeleteBehaviour_Command = new RelayCommand(FmpDt_DeleteBehaviour);
        FmpDt_LbBehaviours_BtDelete_Command = new RelayCommand<Behaviour>(FmpDt_LbBehaviours_BtDelete);
        FmpDt_LbBehaviours_BtMoveUp_Command = new RelayCommand(FmpDt_LbBehaviours_BtUp);
        FmpDt_LbBehaviours_BtMoveDown_Command = new RelayCommand(FmpDt_LbBehaviours_BtDown);

        FmpDt_CreateTarget_Command = new RelayCommand(FmpDt_CreateTarget);
        FmpDt_DeleteTarget_Command = new RelayCommand(FmpDt_DeleteTarget);
        FmpDt_LbTargets_BtDelete_Command = new RelayCommand<Target>(FmpDt_LbTargets_BtDelete);
        FmpDt_LbTargets_BtMoveUp_Command = new RelayCommand(FmpDt_LbTargets_BtUp);
        FmpDt_LbTargets_BtMoveDown_Command = new RelayCommand(FmpDt_LbTargets_BtDown);

        UcTg_SetFolderPath_Command = new RelayCommand(UcTg_SetFolderPath);
    }


    #region FmpOv_Functions

    private void FmpOv_BtCreate_Fmp()
    {
        FileManagingProfiles.Add(new FileManagingProfile(FmpOv_TbName));
    }

    private void FmpOv_Lb_SelectedItem_Delete()
    {
        FileManagingProfiles.Remove(FmpOv_LbSelectedItem);
    }

    private void FmpOv_Lb_SelectedItem_Duplicate()
    {
        FileManagingProfiles.Add((FileManagingProfile)FmpOv_LbSelectedItem.Clone());
    }

    private void FmpOv_Lb_BtDelete(FileManagingProfile fileManagingProfile)
    {
        FileManagingProfiles.Remove(fileManagingProfile);
    }

    #endregion

    #region FmpDt_Functions

    public void FmpDt_CreateBehaviour()
    {
        FmpOv_LbSelectedItem.AddBehaviour();
    }
    public void FmpDt_DeleteBehaviour()
    {
        FmpOv_LbSelectedItem.RemoveBehaviour(FmpDt_LbBehaviourSelectedItem);
    }

    private void FmpDt_LbBehaviours_BtDelete(Behaviour behaviour)
    {
        FmpOv_LbSelectedItem.RemoveBehaviour(behaviour);
    }

    private void FmpDt_LbBehaviours_BtUp()
    {
        int indexOfSelectedTarget = FmpOv_LbSelectedItem.Behaviours.IndexOf(FmpDt_LbBehaviourSelectedItem);

        if (indexOfSelectedTarget > 0)
        {
            FmpOv_LbSelectedItem.Behaviours.Move(indexOfSelectedTarget, indexOfSelectedTarget - 1);
        }
    }

    private void FmpDt_LbBehaviours_BtDown()
    {
        int indexOfSelectedTarget = FmpOv_LbSelectedItem.Behaviours.IndexOf(FmpDt_LbBehaviourSelectedItem);

        if (indexOfSelectedTarget < FmpOv_LbSelectedItem.Behaviours.Count - 1)
        {
            FmpOv_LbSelectedItem.Behaviours.Move(indexOfSelectedTarget, indexOfSelectedTarget + 1);
        }
    }


    public void FmpDt_CreateTarget()
    {
        OpenFolderDialog openFolderDialog = new OpenFolderDialog();
        if (openFolderDialog.ShowDialog() == true && openFolderDialog.FolderName != "")
            FmpOv_LbSelectedItem.AddTarget(openFolderDialog.FolderName);
    }
    public void FmpDt_DeleteTarget()
    {
        FmpOv_LbSelectedItem.RemoveTarget(FmpDt_LbTargetSelectedItem);
    }

    private void FmpDt_LbTargets_BtDelete(Target target)
    {
        FmpOv_LbSelectedItem.RemoveTarget(target);
    }

    private void FmpDt_LbTargets_BtUp()
    {
        int indexOfSelectedTarget = FmpOv_LbSelectedItem.Targets.IndexOf(FmpDt_LbTargetSelectedItem);

        if (indexOfSelectedTarget > 0)
        {
            FmpOv_LbSelectedItem.Targets.Move(indexOfSelectedTarget, indexOfSelectedTarget - 1);
        }
    }

    private void FmpDt_LbTargets_BtDown()
    {
        int indexOfSelectedTarget = FmpOv_LbSelectedItem.Targets.IndexOf(FmpDt_LbTargetSelectedItem);

        if (indexOfSelectedTarget < FmpOv_LbSelectedItem.Targets.Count - 1)
        {
            FmpOv_LbSelectedItem.Targets.Move(indexOfSelectedTarget, indexOfSelectedTarget + 1);
        }
    }

    #endregion

    #region UcBh_Functions



    #endregion

    #region UcTg_Functions

    private void UcTg_SetFolderPath()
    {
        OpenFolderDialog openFolderDialog = new OpenFolderDialog();
        if (openFolderDialog.ShowDialog() == true)
            FmpDt_LbTargetSelectedItem.FolderPath = openFolderDialog.FolderName;
    }

    #endregion

    private void LbFileManagingProfiles_KeyDuplicate(ListBox lbFileManagingProfiles, KeyEventArgs e)
    {
        if (lbFileManagingProfiles.SelectedItem is FileManagingProfile fileManagingProfile &&
            lbFileManagingProfiles.SelectedItems is ObservableCollection<FileManagingProfile> fileManagingProfiles)
        {
            string origName = fileManagingProfile.Name;

            fileManagingProfile.Name = origName + "_Duplicate";

            fileManagingProfiles.Add(fileManagingProfile);
        }
    }

    #region Properties

    public ObservableCollection<FileManagingProfile> FileManagingProfiles
    {
        get => _fileManagingProfiles;
        set
        {
            if (Equals(value, _fileManagingProfiles)) return;
            _fileManagingProfiles = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    #endregion
}