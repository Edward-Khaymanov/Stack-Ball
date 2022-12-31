using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class LevelEventHandler : MonoBehaviour
{
    private Ball _ball;
    private BallSkinProvider _ballSkinProvider;
    private CameraMover _camera;
    private Level _level;
    private LevelProgressBar _levelProgressBar;
    private LevelProvider _levelProvider;
    private LoseScreen _loseScreen;
    private PlatformGenerator _platformGenerator;
    private Player _player;
    private SkyboxView _skybox;
    private TextView _scoreView;
    private WinScreen _winScreen;


    [Inject]
    private void Constructor(
        Ball ball, 
        BallSkinProvider ballSkinProvider,
        CameraMover camera, 
        LevelProgressBar levelProgressBar, 
        LoseScreen loseScreen, 
        PlatformGenerator platformGenerator,
        SkyboxView skybox,
        [Inject(Id = DIMarkers.SCORE_VIEW)] TextView scoreView, 
        WinScreen winScreen)
    {
        _ball = ball;
        _ballSkinProvider = ballSkinProvider;
        _camera = camera;
        _loseScreen = loseScreen;
        _platformGenerator = platformGenerator;
        _levelProgressBar = levelProgressBar;
        _skybox = skybox;
        _scoreView = scoreView;
        _winScreen = winScreen;
    }

    private void Start()
    {
        _player = Player.Current;
        _levelProvider = LevelProvider.Instance;
        _level = Level.Current;
        BuildLevel();
    }

    private void OnEnable()
    {
        EventsHolder.LevelBuilding += BuildLevel;
        EventsHolder.LevelPassed += OnLevelPassed;
        EventsHolder.Losed += OnLose;
        EventsHolder.PlatformDestroyed += OnPlatformDestroyed;
        EventsHolder.SkinUpdated += UpdatePlayerData;
    }

    private void OnDisable()
    {
        EventsHolder.LevelBuilding -= BuildLevel;
        EventsHolder.LevelPassed -= OnLevelPassed;
        EventsHolder.Losed -= OnLose;
        EventsHolder.PlatformDestroyed -= OnPlatformDestroyed;
        EventsHolder.SkinUpdated -= UpdatePlayerData;
    }

    private void BuildLevel()
    {
        _platformGenerator.Build(_level.Platform, _level.ColorPalette.Platform);
        _skybox.Render(_level.ColorPalette.Background);
        _levelProgressBar.Render(_level.ColorPalette.Main, _level.Number);
        _ball.ResetBall(_player.ChoicedSkin, _level.ColorPalette.Main);
        _camera.ResetMover();
        _scoreView.Render(_player.Score.CurrentScore.ToString());
        EventsHolder.StartLevel();
    }

    private void OnLevelPassed()
    {
        _winScreen.Render(_level.Number);
        _level = new Level(_level.Number + 1, _levelProvider.GetRandomPlatformKey(), _levelProvider.GetRandomColorPaletteKey());
        _level.LoadProperties();
        _level.Save();
        _player.Save();
    }

    private void OnLose()
    {
        _player.Score.TrySetBestScore();
        _loseScreen.Render(_player.Score.CurrentScore, _player.Score.BestScore);
        _player.Score.ResetCurrentScore();
        _player.Save();
    }

    private void OnPlatformDestroyed()
    {
        AddScore();
        CalculateProgress();
    }

    private void AddScore()
    {
        var scorePoints = 1;
        if (_ball.IsInvincible)
            scorePoints = 2;

        _player.Score.AddScorePoints(scorePoints);
        _scoreView.Render(_player.Score.CurrentScore.ToString());
    }

    private void CalculateProgress()
    {
        _levelProgressBar.Fill(_platformGenerator.PlatformsCount);
    }
    
    private void UpdatePlayerData()
    {
        _ball.UpdateView(_player.ChoicedSkin, _level.ColorPalette.Main);
    }
}
