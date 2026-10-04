
# Application Layer
`	API
`	UI
`	Infrastructure
*		- ARepository
*		- AUnitOfWork
`	DB
`	Jobs
`	Stream
# 	ApplicationCore
`		Application Services
*			AEntityNamed
*			AEntityHistory
#			DLK.DomainServices.Repository.Abstraction
*					- ARepository
*					- AUnitOfWork
*					- ARepository
*					- AUnitOfWork
#			DLK.DomainServices.Repository.Interfaces.DSL
*					- IRepository
*					- IUnitOfWork
#			DLK.DomainModel.Entities.Abstraction
*				- AEntity
*				- AEntityNamed
*				- AEntityNamedWithHistory
*				- AEntityNamedWithDetails
*				- AEntityNamedWithAccess
*				- AEntityNamedWithSecurity

#	DLK.DomainServices.Repository.DSL
*		- IRepository
*		- IUnitOfWork
#	DLK.DomainModel.Entities.Ordinary
`		History
*			- IEntityCreated
*			- IEntityModified
*			- IRntityDeleted
`		Audit
`		Sequrity
`		Access
#	DLK.DomainModel.Entities.DSL
*		- IEntity
*		- IHasChild
*		- IHasParent

DIADSL
DIaDSL
