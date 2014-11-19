﻿using UnityEngine;
using System.Collections;

public class FilleAnimation : MonoBehaviour {

    private Papy _papy;
    private Animator _animator;

    private bool _holdHands;
    private bool _filleTouch;
    private bool _attached;
    // Use this for initialization
	void Start () {
	    _animator = GetComponent<Animator>();
	    var papy = GameObject.FindGameObjectWithTag("Papy");
	    if (papy)
	        _papy = papy.GetComponent<Papy>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (!_papy) {
	        var papy = GameObject.FindGameObjectWithTag("Papy");
	        if (papy)
	            _papy = papy.GetComponent<Papy>();
	    }

	    if (!_papy)
	        return;

	    if ((_holdHands != _papy.holdHand) || (_filleTouch != _papy.filleTouche)) {
            
	        if (_papy.holdHand && _papy.filleTouche) {
                networkView.RPC("HoldHands", RPCMode.All);
	            _papy.callHide();
	            _attached = true;
	        }
            else if ((_holdHands != _papy.holdHand) && !_papy.holdHand && _attached) {
                _attached = false;
                networkView.RPC("LetGo", RPCMode.All);
                _papy.callShow(transform);
	        }
            _holdHands = _papy.holdHand;
	        _filleTouch = _papy.filleTouche;
	    }

	}

    [RPC]
    void HoldHands() {
        _animator.SetTrigger("holdHands");
    }

    [RPC]
    void LetGo() {
        _animator.SetTrigger("letGo");
    }


}