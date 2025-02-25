# Survival-Shooter
### 개요

<div align="center">
  <img src="https://github.com/user-attachments/assets/097107ac-4167-490a-bfa4-590d2932eb6d" width="98%" height="100%"/>
  <img src="https://github.com/user-attachments/assets/8efc0c7a-a700-4600-830b-5eb907875d99" width="49%" height="50%"/>
  <img src="https://github.com/user-attachments/assets/ec17c06a-df17-4c12-908e-d924ba621df0" width="49%" height="50%"/>
</div>

 + 1분동안 언데드에게 살아남아 점수를 쌓아 올리는 간단한 쿼터 뷰 슈팅 게임입니다.

## 개발 환경
+ Unity Version : 2022.3.34f1 LTS
+ 더 이상 Unity Store에서 구할 수 없는 에셋입니다.
+ 개발기간 : 2024.10 ~ 2024.11(약 1개월)
+ 개발 및 테스트 플레이 컴퓨터 사양
  - GPU : Radeon 7900XTX
  - CPU : Razen 7950X
  - Ram : DDR5 32GB

## 기술
 + Manager Script
    + CanvasManager : 각 캠버스에서 패널 전환을 관리 하거나 캔버스 스크립트에 사용될 공통 함수들을 관리합니다.
    + SoundManager : 설정 화면에서 사운드 설정 값을 받아와 믹서의 볼륨 값을 조절합니다. 싱글톤이며 처음 게임 시작하고 파괴되지 않습니다.
    + GameManager : 적의 스폰, 라운드 타임, 스코어, 게임 상태를 관리합니다. GameScene에만 존재하며 싱글톤입니다.
 + 플레이어 및 적 상태, 동작, 상호작용
    + 플레이어 및 적들의 상태, 동작, 상호작용은 프리팹에 적용되어 있습니다.

<div align="center">
  <img src="https://github.com/user-attachments/assets/13f24072-44c3-4e75-966b-0a33ae9312d8"/>

  플레이어
</div>

  + Player_Prefab
    + PlayerState : 플레이어의 체력, 플레이어 체력 상태에 따른 상호작용을 관리합니다.
    + PlayerGaze : 플레이어 오브젝트가 마우스 방향으로 바라보게 만듭니다.
    + PlayerMove : 키 입력에 따른 플레이어 오브젝트의 움직임에 대해 관리합니다.
    + PlayerGunFire : 플레이어 오브젝트에서 Gun > GunBarrelEnd 오브젝트에 붙어있는 스크립트로 총 발사 간격, 데미지 등 마우스 클릭시 총 사격에 대해 관리를 합니다.

<div align="center">
  <img src="https://github.com/user-attachments/assets/42ad073e-c2d6-4eaf-9bc1-2a89a6a09ac6"/>

  적
</div>

  + Zombie_Prefabs(Hellphant, ZomBeer, ZomBunny)
    + EnemyState : 적의 공격 데미지, 공격 간격, 체력, 적의 스코어 등을 관리합니다.
    + 적의 이동은 "NavMeshAgent"를 사용하여 플레이어를 찾아 자동으로 추적합니다.

## 영상
https://youtu.be/F90QAPxeS5Q

## 다운로드
https://github.com/Choigyungyun/Survival_Shooter/tree/main/Test_Build
