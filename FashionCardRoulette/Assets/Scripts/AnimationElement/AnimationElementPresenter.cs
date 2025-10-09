using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationElementPresenter : IAnimationElementProvider
{
    private readonly AnimationElementModel _model;
    private readonly AnimationElementView _view;

    public AnimationElementPresenter(AnimationElementModel model, AnimationElementView view)
    {
        _model = model; _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnActivateAnimation += _view.Activate;
        _model.OnDeactivateAnimation += _view.Deactivate;
    }

    private void DeactivateEvents()
    {
        _model.OnActivateAnimation -= _view.Activate;
        _model.OnDeactivateAnimation -= _view.Deactivate;
    }

    #region Input

    public void Activate(string id, int cycles = 1)
    {
        _model.Activate(id, cycles);
    }

    public void Deactivate(string id)
    {
        _model.Deactivate(id);
    }

    #endregion
}

public interface IAnimationElementProvider
{
    /// <summary>
    /// ��������� �������� � ������ ID ��������
    /// </summary>
    /// <param name="id">ID ��������</param>
    /// <param name="cycles">���������� ������ ��������, �� ��������� = 1</param>
    public void Activate(string id, int cycles = 1);

    /// <summary>
    /// ����������� �������� � ������ ID ��������
    /// </summary>
    /// <param name="id"></param>
    public void Deactivate(string id);
}
