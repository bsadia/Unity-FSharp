﻿namespace UnityFunctional
open UnityEngine
open System
open Interfaces

type FRP_Shooter() =
    inherit FRPBehaviour()
    
    [<SerializeField>]
    let mutable Speed = 0.0f
    [<SerializeField>]
    let mutable RotationSpeed = 25.0f

    let mutable shotsBeforeStateChange:int = 5
    let mutable attackTarget:Transform = null
    let mutable moveTarget:Vector3 = Vector3.zero
    let mutable cooldowner = 0.0f

    interface IStateMachineEntity with
        member this.name = base.name
        [<SerializeField>]
        member this.Speed = Speed
        [<SerializeField>]
        member this.RotationSpeed= RotationSpeed
        [<SerializeField>]
        member this.ShotsBeforeStateChange
            with get() = shotsBeforeStateChange
            and set(v)= shotsBeforeStateChange <- v
        [<SerializeField>]
        member this.AttackTarget
            with get() = attackTarget
            and set(v) = attackTarget <- v
        [<SerializeField>]
        member this.MoveTarget
            with get() = moveTarget
            and set(v) = moveTarget <- v
        member this.ShotCooldown = 2.0f
        member this.Cooldowner
            with get() = cooldowner
            and set(v) = cooldowner <- v
        member this.transform
            with get() = this.transform

    member this.Start() =
        let stateMachineGO = GameObject.FindGameObjectsWithTag("StateMachine").[1]
        let stateMachine = stateMachineGO.GetComponent<FRP_StateMachine>()
        stateMachine.JoinState this State.Moving

        this.ReactTo<Collision> (FRPEvent.CollisionEnter, 
            (fun c -> 
                let shot = c.collider.GetComponent<FRP_Shot>()
                not (shot = null) && shot.HasExitedSpawnerCollider
                ),
            (fun c -> 
                GameObject.FindGameObjectWithTag("StateMachine").GetComponent<FRP_StateMachine>().TransferState this State.Fleeing))

        this.ReactTo<Collision> (FRPEvent.CollisionEnter,
            (fun c -> c.collider.name = "Boundary"),
            (fun c -> this.transform.Rotate(0.0f, 180.0f, 0.0f))
        )