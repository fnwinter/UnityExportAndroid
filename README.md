# Export Android Project with Unity

- Unity로 안드로이드 프로젝트를 개발 하다보면,
직접 Unity의 안드로이드 소스 코드를 수정할 필요가 있는데,
그러면 계속 수동으로 기존 assets 폴더에 새로 export 된 assets 폴더를 복사를
해줘야함.
- 그래서 export 후에 자동으로 assets 폴더의 파일들을 복사할 수 있도록 스크립트를 작성함.

## 사용법
- Unity 프로젝트에 exportAndroid.cs Script를 import 함
- exportAndroid.cs의 androidProjectPath 를 android project path로 변경
- .gitignore에 exported_temp 폴더를 추가
- 안드로이드 빌드시에 Ctrl+Shift+E로 빌드 또는 Export 메뉴에서 Export Android 선택
