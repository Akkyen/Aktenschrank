using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
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
    public RelayCommand<Behaviour> SpDt_LbBehaviours_BtDelete_Command { get; }

    public RelayCommand SpDt_LbBehaviours_BtMoveUp_Command { get; }
    public RelayCommand SpDt_LbBehaviours_BtMoveDown_Command { get; }


    public RelayCommand SpDt_CreateTarget_Command { get; }
    public RelayCommand SpDt_DeleteTarget_Command { get; }
    public RelayCommand<Target> SpDt_LbTargets_BtDelete_Command { get; }

    public RelayCommand SpDt_LbTargets_BtMoveUp_Command { get; }
    public RelayCommand SpDt_LbTargets_BtMoveDown_Command { get; }

    #endregion

    #region UcBh_Commands

    public RelayCommand UcBh__Command { get; }

    #endregion

    #region UcTg_Commands

    public RelayCommand UcTg_SetFolderPath_Command { get; }

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
        SpDt_LbBehaviours_BtDelete_Command = new RelayCommand<Behaviour>(SpDt_LbBehaviours_BtDelete);
        SpDt_LbBehaviours_BtMoveUp_Command = new RelayCommand(SpDt_LbBehaviours_BtUp);
        SpDt_LbBehaviours_BtMoveDown_Command = new RelayCommand(SpDt_LbBehaviours_BtDown);

        SpDt_CreateTarget_Command = new RelayCommand(SpDt_CreateTarget);
        SpDt_DeleteTarget_Command = new RelayCommand(SpDt_DeleteTarget);
        SpDt_LbTargets_BtDelete_Command = new RelayCommand<Target>(SpDt_LbTargets_BtDelete);
        SpDt_LbTargets_BtMoveUp_Command = new RelayCommand(SpDt_LbTargets_BtUp);
        SpDt_LbTargets_BtMoveDown_Command = new RelayCommand(SpDt_LbTargets_BtDown);

        UcTg_SetFolderPath_Command = new RelayCommand(UcTg_SetFolderPath);
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
        SpOv_LbSelectedItem.AddBehaviour();
    }
    public void SpDt_DeleteBehaviour()
    {
        SpOv_LbSelectedItem.RemoveBehaviour(SpDt_LbBehaviourSelectedItem);
    }

    private void SpDt_LbBehaviours_BtDelete(Behaviour behaviour)
    {
        SpOv_LbSelectedItem.RemoveBehaviour(behaviour);
    }

    private void SpDt_LbBehaviours_BtUp()
    {
        int indexOfSelectedTarget = SpOv_LbSelectedItem.Behaviours.IndexOf(SpDt_LbBehaviourSelectedItem);

        if (indexOfSelectedTarget > 0)
        {
            SpOv_LbSelectedItem.Behaviours.Move(indexOfSelectedTarget, indexOfSelectedTarget - 1);
        }
    }

    private void SpDt_LbBehaviours_BtDown()
    {
        int indexOfSelectedTarget = SpOv_LbSelectedItem.Behaviours.IndexOf(SpDt_LbBehaviourSelectedItem);

        if (indexOfSelectedTarget < SpOv_LbSelectedItem.Behaviours.Count - 1)
        {
            SpOv_LbSelectedItem.Behaviours.Move(indexOfSelectedTarget, indexOfSelectedTarget + 1);
        }
    }


    public void SpDt_CreateTarget()
    {
        OpenFolderDialog openFolderDialog = new OpenFolderDialog();
        if (openFolderDialog.ShowDialog() == true && openFolderDialog.FolderName != "")
            SpOv_LbSelectedItem.AddTarget(openFolderDialog.FolderName);
    }
    public void SpDt_DeleteTarget()
    {
        SpOv_LbSelectedItem.RemoveTarget(SpDt_LbTargetSelectedItem);
    }

    private void SpDt_LbTargets_BtDelete(Target target)
    {
        SpOv_LbSelectedItem.RemoveTarget(target);
    }

    private void SpDt_LbTargets_BtUp()
    {
        int indexOfSelectedTarget = SpOv_LbSelectedItem.Targets.IndexOf(SpDt_LbTargetSelectedItem);

        if (indexOfSelectedTarget > 0)
        {
            SpOv_LbSelectedItem.Targets.Move(indexOfSelectedTarget, indexOfSelectedTarget - 1);
        }
    }

    private void SpDt_LbTargets_BtDown()
    {
        int indexOfSelectedTarget = SpOv_LbSelectedItem.Targets.IndexOf(SpDt_LbTargetSelectedItem);

        if (indexOfSelectedTarget < SpOv_LbSelectedItem.Targets.Count - 1)
        {
            SpOv_LbSelectedItem.Targets.Move(indexOfSelectedTarget, indexOfSelectedTarget + 1);
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
            SpDt_LbTargetSelectedItem.FolderPath = openFolderDialog.FolderName;
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

    private void OnlyLettersAndNumbers_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("([A-Z])|([a-z])|[0-9]");
        e.Handled = !regex.IsMatch(e.Text);

        if (e.OriginalSource is TextBox textBox)
        {
            BindingExpression binding = textBox.GetBindingExpression(TextBox.TextProperty);
            binding?.UpdateSource();
        }
    }

    #region Utils



    #endregion
}