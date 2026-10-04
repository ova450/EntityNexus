# EntityNexus.Core

EntityNexus DSL Framework Core
------

-----

Это и есть чистая архитектура, или onion

Чтоб ты окончатеельно все понял, излагаю концепцию Kernel

На картинках чаще всего рисуют в качестве нижней луковичной шелухи уровень DomainModel.
Но иногда внутри этого уровня добавляют еще центральный маленький кружок, называя его по разному. Иногда Core
Я обычно называю Kernel.
Не знаю, и знать не хочу, что подразумевается под этим кружком. Моя концепция такова:
Kernel - это яйцо в яйце. Ну точнее лук в луке :)
Архитектура Kernel - это архитектура Domain Layer.
По факту - это параллельных чистых архитектуры, Kernel.Domain -> App.Domain, где App.Domain - это абстракции App, a Kernel.Domain - это абстракции абстракций App.
Т.е.:
Kernel.Domain.DomainModel.IEntity -> Kernel.Domain.DomainModel.AEntity -> App.Domain.DomainModel.EntityRealAbstract->App.Core.EntityReal
Для DomainService аналогично.

Это решение(репо) open-source/, EntityNexus.Kernel (хотя в действительности EntityNexus - это и есть Kernel)

Вообще получается я изобрел новый класс приложений OKaO - Onion Kernel as Onion

И полное название проекта должно быть EntityNexus - Onion Kernel as Onion DSL. Onion in Onion.

И тогда структура решений/глобальных пакетов/неймспейсов будет такая:

EntityNexus.Core.DomainLayer.DomainModel						глоб.пакет  EntityNexus.Core
EntityNexus.Core.DomainLayer.DomainModel.Interfaces				внутр.пакет EN.Core.DomainModel.Interfaces
EntityNexus.Core.DomainLayer.DomainModel.AbstractClasses		внутр.пакет EN.Core.DomainModel.AbstractClasses
EntityNexus.Core.DomainLayer.DomainService.Interfaces			внутр.пакет EN.Core.DomainService.Interfaces		(empty)
EntityNexus.Core.DomainLayer.DomainService.AbstractClasses		внутр.пакет EN.Core.DomainService.AbstractClasses	(empty)

EntityNexus.Expansions.DomainLayer.DomainModel						глоб.пакет  EntityNexus.Expansions
EntityNexus.Expansions.DomainLayer.DomainModel.Interfaces			внутр.пакет EN.Expansions.DomainModel.Interfaces
EntityNexus.Expansions.DomainLayer.DomainModel.AbstractClasses		внутр.пакет EN.Expansions.DomainModel.AbstractClasses
EntityNexus.Expansions.DomainLayer.DomainService.Interfaces			внутр.пакет EN.Expansions.DomainService.Interfaces	
EntityNexus.Expansions.DomainLayer.DomainService.AbstractClasses	внутр.пакет EN.Expansions.DomainService.AbstractClasses

EntityNexus.Features.DomainLayer.DomainModel					глоб.пакет  EntityNexus.Features
EntityNexus.Features.DomainLayer.DomainModel.Interfaces			внутр.пакет EN.Features.DomainModel.Interfaces
EntityNexus.Features.DomainLayer.DomainModel.AbstractClasses	внутр.пакет EN.Features.DomainModel.AbstractClasses
EntityNexus.Features.DomainLayer.DomainService.Interfaces		внутр.пакет EN.Features.DomainService.Interfaces	
EntityNexus.Features.DomainLayer.DomainService.AbstractClasses	внутр.пакет EN.Features.DomainService.AbstractClasses

EntityNexus.Enterprise.DomainLayer.DomainModel						глоб.пакет  EntityNexus.Enterprise
EntityNexus.Enterprise.DomainLayer.DomainModel.Interfaces			внутр.пакет EN.Enterprise.DomainModel.Interfaces
EntityNexus.Enterprise.DomainLayer.DomainModel.AbstractClasses		внутр.пакет EN.Enterprise.DomainModel.AbstractClasses
EntityNexus.Enterprise.DomainLayer.DomainService.Interfaces			внутр.пакет EN.Enterprise.DomainService.Interfaces	
EntityNexus.Enterprise.DomainLayer.DomainService.AbstractClasses	внутр.пакет EN.Enterprise.DomainService.AbstractClasses

Вот это полная структура пакетов и нейминга EntityNexus.

Но вообще эта тема другого чата. Здесь - гитхаб. Поэтому - запомни все, что выше, как концепцию EntityNexus - Onion Kernel as Onion DSL. Onion in Onion. OKasOD.

что касается гитхаба, сейчас мы работаем в EntityNexus.Core, и вроде все получается неплохо. Поэтому продолжим, с учетом изложенного.