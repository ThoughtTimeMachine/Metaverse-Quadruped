using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStateGoToPlayer : PetBehaviorState
{
    private Transform _player;
    public PetStateGoToPlayer(PetBehaviorSystem petBehaviourSystem) : base(petBehaviourSystem)
    {

    }
    public override void Start()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            if (_player == null ) { return; }
        }
            GoToPlayer();
            _petBehaviorSystem._petController.ChangeAnimationState("Movement", 0);
    }

    private void GoToPlayer()
    {
        _petBehaviorSystem._petController.SetDestination(_player);
    }
}
