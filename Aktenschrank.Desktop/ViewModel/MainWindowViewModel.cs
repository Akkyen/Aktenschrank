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
    private ObservableCollection<SortingProfile> _sortingProfiles = new();

    #region SpOv_Commands

    public RelayCommand SpOv_Create_Sp_Command { get; }

    public RelayCommand SpOv_Lb_SelectedItem_Delete_Command { get; }
    public RelayCommand SpOv_Lb_SelectedItem_Duplicate_Command { get; }

    public RelayCommand<SortingProfile> SpOv_Lb_BtDelete_Command { get; }

    #endregion

    #region SpDt_Commands

    public RelayCommand SpDt_CreateBehaviour_Command { get; }
    public RelayCommand SpDt_DeleteBehaviour_Command { get; }

    public RelayCommand SpDt_CreateTarget_Command { get; }
    public RelayCommand SpDt_DeleteTarget_Command { get; }

    #endregion

    #region Bindings_SortingProfile_Overview

    private string _spOv_TbName;
    private SortingProfile _spOv_LbSelectedItem;

    #region Properties

    public string SpOv_TbName
    {
        get => _spOv_TbName;
        set
        {
            if (value == _spOv_TbName) return;
            _spOv_TbName = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public SortingProfile SpOv_LbSelectedItem
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

    #region Bindings_SortingProfile_Details

    private Behaviour _spDt_LbBehaviourSelectedItem;
    private Target _spDt_LbTargetSelectedItem;

    #region Properties

    public Behaviour SpDt_LbBehaviourSelectedItem
    {
        get => _spDt_LbBehaviourSelectedItem;
        set
        {
            if (Equals(value, _spDt_LbBehaviourSelectedItem)) return;
            _spDt_LbBehaviourSelectedItem = value;
            OnPropertyChanged();
        }
    }

    public Target SpDt_LbTargetSelectedItem
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
        SpOv_Create_Sp_Command = new RelayCommand(SpOv_BtCreate_Sp);

        SpOv_Lb_SelectedItem_Delete_Command = new RelayCommand(SpOv_Lb_SelectedItem_Delete);
        SpOv_Lb_SelectedItem_Duplicate_Command = new RelayCommand(SpOv_Lb_SelectedItem_Duplicate);

        SpOv_Lb_BtDelete_Command = new RelayCommand<SortingProfile>(SpOv_Lb_BtDelete);


        SpDt_CreateBehaviour_Command = new RelayCommand(SpDt_CreateBehaviour);
        SpDt_DeleteBehaviour_Command = new RelayCommand(SpDt_DeleteBehaviour);

        SpDt_CreateTarget_Command = new RelayCommand(SpDt_CreateTarget);
        SpDt_DeleteTarget_Command = new RelayCommand(SpDt_DeleteTarget);
    }

    #region SpOv_Functions

    private void SpOv_BtCreate_Sp()
    {
        SortingProfiles.Add(new SortingProfile(SpOv_TbName));
    }

    private void SpOv_Lb_SelectedItem_Delete()
    {
        SortingProfiles.Remove(SpOv_LbSelectedItem);
    }

    private void SpOv_Lb_SelectedItem_Duplicate()
    {
        SortingProfiles.Add((SortingProfile)SpOv_LbSelectedItem.Clone());
    }

    private void SpOv_Lb_BtDelete(SortingProfile sortingProfile)
    {
        SortingProfiles.Remove(sortingProfile);
    }

    #endregion

    #region SpDt_Functions

    public void SpDt_CreateBehaviour()
    {
        SpOv_LbSelectedItem.Behaviours.Add(new Behaviour("Behaviour"));
    }
    public void SpDt_DeleteBehaviour()
    {
        SpOv_LbSelectedItem.Behaviours.Remove(SpDt_LbBehaviourSelectedItem);
    }

    public void SpDt_CreateTarget()
    {
        string folderPath = string.Empty;

        OpenFolderDialog openFolderDialog = new OpenFolderDialog();
        if (openFolderDialog.ShowDialog() == true)
            folderPath = openFolderDialog.FolderName;

        SpOv_LbSelectedItem.Targets.Add(new Target(folderPath));
    }
    public void SpDt_DeleteTarget()
    {
        SpOv_LbSelectedItem.Targets.Remove(SpDt_LbTargetSelectedItem);
    }

    #endregion

    private void LbSortingProfiles_KeyDuplicate(ListBox lbSortingProfiles, KeyEventArgs e)
    {
        if (lbSortingProfiles.SelectedItem is SortingProfile sortingProfile &&
            lbSortingProfiles.SelectedItems is ObservableCollection<SortingProfile> sortingProfiles)
        {
            string origName = sortingProfile.Name;

            sortingProfile.Name = origName + "_Duplicate";

            sortingProfiles.Add(sortingProfile);
        }
    }

    #region Properties

    public ObservableCollection<SortingProfile> SortingProfiles
    {
        get => _sortingProfiles;
        set
        {
            if (Equals(value, _sortingProfiles)) return;
            _sortingProfiles = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    #endregion
}