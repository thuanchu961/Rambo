using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public STATE playerState = STATE.idle;
    public enum STATE
    {
        idle,
        idle_down_attack,
        idle_melee_attack,
        idle_shoot,
        idle_down_shoot,
        idle_up_shoot,
        idle_throw,
        idle_down_throw,
        idle_up_throw,
        walk,
        walk_up,
        walk_down,
        walk_down_shoot,
        walk_up_shoot,
        walk_shoot,
        walk_throw,
        walk_up_throw,
        walk_down_throw,
        jump,
        jump_up_shoot,
        jump_throw,
        jump_shoot,
        down,
        up,
        die,
        _throw,
        shoot,
        len
    }
}
