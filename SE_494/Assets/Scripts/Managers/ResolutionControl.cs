using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ResolutionControl : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] _resolutions; 
    private List<Resolution> _filteredResolutions;

    private float _currentRefreshRate;
    private int _currentResolutionIndex = 0;
    
    void Start()
    {
        _resolutions = Screen.resolutions;
        _filteredResolutions = new List<Resolution>();
        
        resolutionDropdown.ClearOptions();
        _currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            if (_resolutions[i].refreshRate == _currentRefreshRate)
            {
                _filteredResolutions.Add(_resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < _filteredResolutions.Count; i++)
        {
            string resolutionOption = _filteredResolutions[i].width + "x" + _filteredResolutions[i].height + " " + _filteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);

            if (_filteredResolutions[i].width == Screen.width && _filteredResolutions[i].height == Screen.height)
            {
                _currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = _currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
