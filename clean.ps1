# -----------------------------------------
# Clean project
# SPDX-License-Identifier: Apache-2.0
# SPDX-FileCopyrightText: 2021-2022 T. Graf
# -----------------------------------------

dotnet clean
Remove-Item "Tethys.Xsd\bin" -Recurse
Remove-Item "Tethys.Xsd\obj" -Recurse