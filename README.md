# 🐧 Newton — 2D Penguin Survival Game

Unity와 C#으로 제작한 **2D 캐주얼 생존 게임**입니다. 플레이어는 펭귄을 조작해 하늘에서 떨어지는 사과를 피하고, 최대한 오래 생존하며 최고 기록에 도전합니다.

**[ 게임 플레이하기](https://jeongmin80.github.io/unity-game/)** · **[💻 GitHub 저장소](https://github.com/JeongMin80/unity-game)**

##  프로젝트 개요

| 항목 | 내용 |
| --- | --- |
| 게임 이름 | Newton |
| Unity 프로젝트명 | Last Dance |
| 장르 | 2D 캐주얼 서바이벌 / 장애물 회피 |
| 게임 엔진 | Unity 2021.3.45f2 |
| 개발 언어 | C# |
| 개발 환경 | Visual Studio 2022 |
| 배포 환경 | Unity WebGL, GitHub Pages |
| 화면 비율 | 세로형 9:16 |

##  게임 소개

- 빙하 위의 펭귄을 좌우로 움직여 떨어지는 사과를 피합니다.
- 살아남은 시간이 점수로 기록되며, 오래 버틸수록 난이도가 높아집니다.
- 게임 종료 후 생존 시간과 최고 기록을 확인하고 다시 도전할 수 있습니다.

> **플레이 환경:** PC의 Chrome 또는 Edge 브라우저에서 [게임 실행하기](https://jeongmin80.github.io/unity-game/). 웹페이지의 전체 화면 버튼을 이용하면 더 크게 플레이할 수 있습니다.

##  주요 구현 기능

### 플레이어 및 충돌 처리
- C# 기반 플레이어 이동 및 입력 처리
- 펭귄 캐릭터의 2D 스프라이트 애니메이션 적용
- 사과와 플레이어의 충돌 판정 및 게임 오버 처리

### 사과 생성 및 난이도 시스템
- 무작위 위치에서 사과 생성 및 낙하
- 생존 시간에 따른 단계별 생성 개수·간격 조절
- 연속 사과 생성과 중력 배율을 이용한 난이도 변화

### 게임 진행 및 UI
- **Title → Playing → Game Over** 상태 전환
- 생존 시간 측정 및 최고 기록 저장·표시
- 게임 시작, 재시작 및 종료 UI
- 상점 화면과 사과 스킨(예: Ice, Fire, Skull) 구성

##  사용 기술 및 주요 스크립트

| 기술/스크립트 | 역할 |
| --- | --- |
| Unity 2D | 씬 구성, 물리 및 애니메이션 처리 |
| C# | 게임 로직과 오브젝트 동작 구현 |
| `GameManager` | 게임 상태, 타이머 및 게임 종료 관리 |
| `PlayerController` | 플레이어 입력과 이동 제어 |
| `AppleSpawner` | 사과 생성 패턴 및 난이도 조절 |
| WebGL / GitHub Pages | 브라우저 플레이용 빌드 및 정적 배포 |

##  프로젝트 구조

```text
unity-game/
├── Assets/             # 씬, C# 스크립트, 프리팹 등 게임 리소스
├── Packages/           # Unity 패키지 의존성
├── ProjectSettings/    # Unity 프로젝트 설정
├── docs/               # GitHub Pages에 배포되는 WebGL 빌드
├── .gitignore
└── README.md
```

##  실행 방법

### 브라우저에서 실행

**[Newton 바로 플레이하기](https://jeongmin80.github.io/unity-game/)**

별도의 게임 설치 없이 PC 브라우저에서 실행할 수 있습니다.

### Unity Editor에서 실행

1. 저장소를 내려받습니다.
   ```bash
   git clone https://github.com/JeongMin80/unity-game.git
   ```
2. Unity Hub에서 다운로드한 폴더를 엽니다.
3. **Unity 2021.3.45f2** 버전으로 프로젝트를 실행합니다.
4. 필요한 외부 에셋이 누락되어 있다면 해당 에셋을 라이선스에 따라 별도로 준비합니다.
5. `SampleScene`을 열고 **Play** 버튼을 누릅니다.

##  개발 경험

- Unity 2D에서 캐릭터 이동, 충돌 판정 및 오브젝트 생성 구현
- 게임 상태 관리와 시간 기반 난이도 설계
- UI·애니메이션을 연동한 게임 흐름 구성
- Unity WebGL 빌드 및 GitHub Pages를 이용한 웹 배포
- Git/GitHub를 이용한 프로젝트 버전 관리

##  에셋 및 라이선스

프로젝트에 사용된 일부 아트·애니메이션 등은 외부 에셋일 수 있으며, 해당 에셋의 권리는 원저작자에게 있습니다. 외부 에셋은 각각의 라이선스 조건에 따라 사용해야 하며, 원본 파일의 재배포가 허용되지 않는 경우 저장소에 포함해서는 안 됩니다.

---

**Newton** · Built with Unity & C#
