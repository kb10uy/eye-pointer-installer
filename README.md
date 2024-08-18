# Automatic EyePointer Installer

[Siromori 氏の EyePointer](https://booth.pm/ja/items/4742883) のセットアップを非破壊に自動化します。具体的には Eye ボーンへの Aim Constraint の設定、角度を正規化したボーンの作成・差し替えをビルド時に実行します。
特にボーンの差し替えは手動で実行する場合 Unpack Prefab が必要なため、これを省略できるのは大きなメリットになります。

## 使い方
**EyePointer のプレハブをアバタールートに置いた後にそのプレハブに "Automate EyePointer Install" コンポーネントを追加するだけ！**
